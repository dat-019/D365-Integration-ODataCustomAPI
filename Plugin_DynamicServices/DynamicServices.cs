//using APIService.Provider;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_DynamicServices
{
    public class DynamicServices : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            try
            {
                tracer.Trace("BEGINNING");
                string Request = context.InputParameters[Constants.Parameters.Request].ToString();
                if (Request.Length == 0)
                    throw new InvalidPluginExecutionException(Constants.ErrMessage.RequiredRequest);
                RequestModel res = Extension.DeSerializeDictionary<RequestModel>(Request);
                IOrganizationService service = factory.CreateOrganizationService(context.UserId);
                Guid response = Guid.Empty;
                bool disabled = false;
                bool requiredLogout = false;
                //CheckUser(service, res.userPortal, ref disabled, ref requiredLogout);
                if (disabled)
                    throw new InvalidPluginExecutionException(Constants.ErrMessage.DisabledUser);
                else if (requiredLogout)
                    throw new InvalidPluginExecutionException(Constants.ErrMessage.RequiredLogout);

                tracer.Trace("request name:: " + res.requestName);

                switch (res.requestName)
                {
                    case Constants.Message.Create:
                        {
                            tracer.Trace("CREATE:: " + res.data);
                            RecordModel2 data = Extension.DeSerializeDictionary<RecordModel2>(res.data);
                            Entity entity = new Entity(data.LogicalName);
                            //Dictionary<string, FieldModel> lsField = data.Field;
                            List<FieldModel> lsField = data.Fields;
                            SetValue(ref entity, lsField, tracer);
                            if (res.owner.ToString() == string.Empty)
                                throw new InvalidPluginExecutionException("ownerid is required!");
                            if (!entity.Contains("ownerid"))
                                entity["ownerid"] = new EntityReference("systemuser", new Guid(res.owner.ToString()));
                            response = service.Create(entity);
                            context.OutputParameters[Constants.Parameters.Reponse] = response.ToString();
                            break;
                        }
                    case Constants.Message.Update:
                        {
                            //throw new Exception("Hello");
                            RecordModel2 data = Extension.DeSerializeDictionary<RecordModel2>(res.data);
                            Entity entity = new Entity(data.LogicalName);
                            entity.Id = new Guid(data.Id);
                            //Dictionary<string, FieldModel> lsField = data.Field;
                            List<FieldModel> lsField = data.Fields;
                            SetValue(ref entity, lsField, tracer);
                            service.Update(entity);
                            response = new Guid(data.Id);
                            break;
                        }
                    case Constants.Message.Delete:
                        {
                            RecordModel data = Extension.DeSerializeDictionary<RecordModel>(res.data);
                            service.Delete(data.LogicalName, new Guid(data.Id));
                            break;
                        }
                    case Constants.Message.Associate:
                        {
                            ExcuteNNRecord data = Extension.DeSerializeDictionary<ExcuteNNRecord>(res.data);
                            EntityReferenceCollection list = ConvertListEntityReferenceCollection(data.ListEntity);
                            service.Associate(data.LogicalName, new Guid(data.Id), new Relationship(data.RelationShip), list);
                            break;
                        }
                    case Constants.Message.Disassociate:
                        {
                            ExcuteNNRecord data = Extension.DeSerializeDictionary<ExcuteNNRecord>(res.data);
                            EntityReferenceCollection list = ConvertListEntityReferenceCollection(data.ListEntity);
                            service.Disassociate(data.LogicalName, new Guid(data.Id), new Relationship(data.RelationShip), list);
                            break;
                        }
                    case Constants.Message.Fetch:
                        {
                            tracer.Trace("Come to Fetch string");
                            EntityCollection en = service.RetrieveMultiple(new FetchExpression(res.data));
                            List<RecordModel> ls_Record = new List<RecordModel>();
                            PageInfor pageinfor = new PageInfor()
                            {
                                MoreRecords = en.MoreRecords,
                                PagingCookie = en.PagingCookie,
                                TotalRecordCount = en.TotalRecordCount
                            };

                            tracer.Trace("Number of records:: " + en.Entities.Count());

                            foreach (Entity entity in en.Entities)
                            {
                                RecordModel record = new RecordModel();
                                record.LogicalName = entity.LogicalName;
                                record.Id = entity.Id.ToString();
                                Dictionary<string, FieldModel> ls_Field = new Dictionary<string, FieldModel>();

                                foreach (var att in entity.Attributes)
                                {
                                    FieldModel field = new FieldModel();
                                    field.FieldName = att.Key;
                                    field.FieldType = att.Value.GetType().Name;
                                    FieldValue value = new FieldValue();
                                    switch (field.FieldType)
                                    {
                                        case Constants.FieldType.Boolean:
                                            {

                                                value.Value = (bool)att.Value;
                                                value.Formated = entity.FormattedValues[att.Key];
                                                break;
                                            }
                                        case Constants.FieldType.PickList:
                                            {
                                                value.Value = ((OptionSetValue)att.Value).Value;
                                                value.Formated = entity.FormattedValues[att.Key];
                                                break;
                                            }
                                        case Constants.FieldType.String:
                                        case Constants.FieldType.Memo:
                                            {
                                                value.Value = att.Value;
                                                break;
                                            }
                                        case Constants.FieldType.Decimal:
                                            {
                                                value.Value = att.Value;
                                                break;
                                            }
                                        case Constants.FieldType.Int:
                                        case Constants.FieldType.Int32:
                                        case Constants.FieldType.Int64:
                                            {
                                                value.Value = att.Value;
                                                break;
                                            }
                                        case Constants.FieldType.DateTime:
                                            {
                                                value.Value = ((DateTime)att.Value).AddHours(7).ToString();
                                                break;
                                            }
                                        case Constants.FieldType.Money:
                                            {
                                                value.Value = ((Money)att.Value).Value;
                                                break;
                                            }
                                        case Constants.FieldType.Lookup:
                                            {
                                                value.LogicalName = ((EntityReference)att.Value).LogicalName;
                                                value.Id = ((EntityReference)att.Value).Id.ToString();
                                                value.Value = ((EntityReference)att.Value).Name;
                                                break;
                                            }
                                        case Constants.FieldType.Guid:
                                            {
                                                value.Value = ((Guid)att.Value).ToString();
                                                break;
                                            }
                                        case Constants.FieldType.Byte:
                                            {
                                                value.Value = Convert.ToBase64String((byte[])att.Value);
                                                break;
                                            }
                                        case Constants.FieldType.AliasedValue:
                                            {
                                                GetAliasValue(ref value, entity, att.Key, (AliasedValue)att.Value);
                                                break;
                                            }
                                        case Constants.FieldType.OptionSetValueCollection:
                                            {
                                                value.Value = string.Join(",", ((OptionSetValueCollection)att.Value).Select(t => t.Value).ToList());
                                                value.Formated = entity.FormattedValues[att.Key];
                                                break;
                                            }
                                        case Constants.FieldType.PartyList:
                                            {
                                                List<PartyListModel> lsParty = new List<PartyListModel>();
                                                EntityCollection collectionEntity = (EntityCollection)att.Value;
                                                foreach (var tmp in collectionEntity.Entities)
                                                {
                                                    PartyListModel party = new PartyListModel();
                                                    party.ActivitypartyId = tmp.Contains(Activityparty.Activitypartyid) ? tmp[Activityparty.Activitypartyid].ToString() : null;
                                                    party.PartyId = tmp.Contains(Activityparty.Partyid) ? ((EntityReference)tmp[Activityparty.Partyid]).Id.ToString() : null;
                                                    party.PartyName = tmp.Contains(Activityparty.Partyid) ? ((EntityReference)tmp[Activityparty.Partyid]).Name.ToString() : null;
                                                    party.PartyLogicalName = tmp.Contains(Activityparty.Partyid) ? ((EntityReference)tmp[Activityparty.Partyid]).LogicalName.ToString() : null;
                                                    lsParty.Add(party);
                                                }
                                                value.Value = Extension.SerializeDictionary(lsParty);
                                                value.LogicalName = att.Key;
                                                break;
                                            }
                                    }
                                    field.FieldValue = value;
                                    ls_Field[att.Key] = field;
                                }
                                record.Field = ls_Field;
                                ls_Record.Add(record);
                            }
                            tracer.Trace("Number of records in results:: " + ls_Record.Count());

                            pageinfor.Results = ls_Record;
                            context.OutputParameters[Constants.Parameters.Reponse] = Extension.SerializeDictionary(pageinfor);
                            break;
                        }
                    case Constants.Message.Login:
                        {
                            service = factory.CreateOrganizationService(null);
                            MD5 md5Hash = MD5.Create();
                            LoginModel loginModel = Extension.DeSerializeObject<LoginModel>(res.data);
                            QueryExpression q = new QueryExpression(Employee.LogicalName);
                            q.ColumnSet = new ColumnSet(Employee.TenDangNhap, Employee.MatKhau, Employee.Systemuser, Employee.Name);
                            q.Criteria.AddCondition(Employee.Statuscode, ConditionOperator.Equal, 1);
                            q.Criteria.AddCondition(Employee.TenDangNhap, ConditionOperator.Equal, loginModel.UserName);
                            q.Criteria.AddCondition(Employee.MatKhau, ConditionOperator.Equal, Extension.GetMd5Hash(md5Hash, loginModel.PassWord));
                            EntityCollection en = service.RetrieveMultiple(q);
                            if (en.Entities.Count > 0)
                            {
                                Entity infor = en.Entities.FirstOrDefault();

                                loginModel = new LoginModel();
                                loginModel.FullName = infor.Contains(Employee.Name) ? infor[Employee.Name].ToString() : string.Empty;
                                loginModel.SystemUser = infor.Contains(Employee.Systemuser) ? ((EntityReference)infor[Employee.Systemuser]).Id.ToString() : string.Empty;
                                loginModel.UserName = infor.Contains(Employee.TenDangNhap) ? infor[Employee.TenDangNhap].ToString() : string.Empty;
                                context.OutputParameters[Constants.Parameters.Reponse] = Extension.SerializeObject<LoginModel>(loginModel);

                            }
                            else
                                context.OutputParameters[Constants.Parameters.Reponse] = Extension.SerializeObject(new ErrModel { Error = "Username or password is incorrect" });

                            break;
                        }
                    case Constants.Message.ChangePassword:
                        {
                            service = factory.CreateOrganizationService(null);
                            MD5 md5Hash = MD5.Create();
                            ChangePasswordModel changePWModel = Extension.DeSerializeObject<ChangePasswordModel>(res.data);
                            QueryExpression q = new QueryExpression(Employee.LogicalName);
                            q.Criteria.AddCondition(Employee.Statuscode, ConditionOperator.Equal, 1);
                            q.Criteria.AddCondition(Employee.TenDangNhap, ConditionOperator.Equal, changePWModel.UserName);
                            EntityCollection en = service.RetrieveMultiple(q);
                            if (en.Entities.Count > 0)
                            {
                                Entity changePW = new Entity(Employee.LogicalName);
                                changePW.Id = en.Entities.FirstOrDefault().Id;
                                changePW[Employee.MatKhau] = changePWModel.PassWordNew;
                                service.Update(changePW);
                            }
                            else
                                context.OutputParameters[Constants.Parameters.Reponse] = Extension.SerializeObject(new ErrModel { Error = "Mật khẩu củ không chính xác!" });

                            break;
                        }
                    case Constants.Message.ChangeState:
                        {
                            Dictionary<string, string> data = Extension.DeSerializeDictionary<Dictionary<string, string>>(res.data);
                            SetStateRequest setStateRequest = new SetStateRequest();
                            setStateRequest.EntityMoniker = new EntityReference(data["logicalName"], Guid.Parse(data["id"]));
                            setStateRequest.State = new OptionSetValue(Int32.Parse(data["statecode"].ToString()));
                            setStateRequest.Status = new OptionSetValue(Int32.Parse(data["statuscode"].ToString()));
                            SetStateResponse setStateResponse = (SetStateResponse)service.Execute(setStateRequest);
                            break;
                        }
                    case Constants.Message.LandingPage:
                        {
                            var model = Extension.DeSerializeObject<LandingPageModel>(res.data);

                            var query = $@"
                        <fetch distinct='true' no-lock='true'>
                          <entity name='campaignactivity'>
                            <attribute name='activityid' />
                            <attribute name='ownerid' />
                            <attribute name='regardingobjectid' />
                            <filter>
                              <condition attribute='activityid' operator='eq' value='{model.CampaignActivityId}'/>
                            </filter>
                          </entity>
                        </fetch>";

                            var result = service.RetrieveMultiple(new FetchExpression(query)).Entities.FirstOrDefault();

                            var addLead = new Entity("lead");
                            addLead["lastname"] = model.FullName;
                            addLead["c30seeds_dienthoaididongchinh"] = model.PhoneNumber;
                            addLead["emailaddress1"] = model.Email;
                            addLead["c30seeds_diachitamtru"] = model.Address;



                            if (result != null)
                            {
                                addLead["ownerid"] = result.GetAttributeValue<EntityReference>("ownerid");
                            }
                            response = service.Create(addLead);

                            EntityCollection lsParty = new EntityCollection();
                            Entity party = new Entity(Activityparty.LogicalName);
                            party[Activityparty.Partyid] = new EntityReference(addLead.LogicalName, response);
                            lsParty.Entities.Add(party);

                            Entity campaignresponse = new Entity("campaignresponse");
                            campaignresponse["subject"] = model.FullName;
                            campaignresponse["ownerid"] = result.GetAttributeValue<EntityReference>("ownerid");
                            campaignresponse["customer"] = lsParty;
                            campaignresponse["regardingobjectid"] = result.GetAttributeValue<EntityReference>("regardingobjectid");
                            campaignresponse["c30seeds_campaignactivityid"] = result.ToEntityReference();



                            service.Create(campaignresponse);

                            context.OutputParameters[Constants.Parameters.Reponse] = response.ToString();
                            break;
                        }
                    default:
                        {
                            throw new InvalidPluginExecutionException(String.Format(Constants.ErrMessage.ResponMessage, res.requestName));
                        }
                }
            }
            catch (InvalidPluginExecutionException ex)
            {
                throw ex;
            }

        }
        private void SetValue(ref Entity entity, List<FieldModel> lsField, ITracingService tracer)
        {
            tracer.Trace("-----SET VALUE-----");
            foreach (FieldModel field in lsField) //FieldModel field in lsField.Values
            {
                FieldValue value = field.FieldValue;
                switch (field.FieldType)
                {
                    case Constants.FieldType.String:
                    case Constants.FieldType.Memo:
                        {
                            if (value.Value != null && value.Value.ToString().Length > 0)
                            {
                                tracer.Trace("Set value - FieldName:: " + field.FieldName + "; Set value - Value:: " + value.Value);
                                entity[field.FieldName] = value.Value;
                            }
                            else
                            {
                                entity[field.FieldName] = null;
                            }
                            break;
                        }
                    case Constants.FieldType.Decimal:
                        {
                            if (value.Value != null && value.Value.ToString().Length > 0)
                                entity[field.FieldName] = Convert.ToDecimal(value.Value);
                            else
                                entity[field.FieldName] = null;
                            break;
                        }
                    case Constants.FieldType.Int:
                        {
                            entity[field.FieldName] = Convert.ToInt16(value.Value);
                            break;
                        }
                    case Constants.FieldType.Int32:
                        {
                            if (value.Value != null && value.Value.ToString().Length > 0)
                                entity[field.FieldName] = Convert.ToInt32(value.Value);
                            else
                                entity[field.FieldName] = null;
                            break;
                        }
                    case Constants.FieldType.Int64:
                        {
                            if (value.Value != null && value.Value.ToString().Length > 0)
                                entity[field.FieldName] = Convert.ToInt64(value.Value);
                            else
                                entity[field.FieldName] = null;
                            break;
                        }
                    case Constants.FieldType.PickList:
                        {
                            if (value.Value != null && value.Value.ToString().Length > 0)
                                entity[field.FieldName] = new OptionSetValue(Convert.ToInt32(value.Value));
                            else
                                entity[field.FieldName] = null;
                            break;
                        }
                    case Constants.FieldType.DateTime:
                        {
                            if (value.Value != null && value.Value.ToString().Length > 0)
                                entity[field.FieldName] = Convert.ToDateTime(value.Value);
                            else
                                entity[field.FieldName] = null;
                            break;
                        }
                    case Constants.FieldType.Double:
                        {
                            entity[field.FieldName] = Convert.ToDouble(value.Value);
                            break;
                        }
                    case Constants.FieldType.Lookup:
                        {
                            if (value != null && value.Id != null)
                            {
                                entity[field.FieldName] = new EntityReference()
                                {
                                    LogicalName = value.LogicalName,
                                    Id = new Guid(value.Id)
                                };
                            }
                            else
                                entity[field.FieldName] = null;
                            break;
                        }
                    case Constants.FieldType.Boolean:
                        {
                            if (value.Value != null && value.Value.ToString().Length > 0)
                                entity[field.FieldName] = Convert.ToBoolean(value.Value);
                            else
                                entity[field.FieldName] = null;
                            break;
                        }
                    case Constants.FieldType.Money:
                        {
                            if (value.Value != null && value.Value.ToString().Length > 0)
                                entity[field.FieldName] = new Money(Convert.ToDecimal(value.Value));
                            else
                                entity[field.FieldName] = null;
                            break;
                        }
                    case Constants.FieldType.OptionSetValueCollection:
                        {
                            if (value.Value != null && value.Value.ToString().Length > 0)
                            {
                                string[] sRes = value.Value.ToString().Split(new char[] { ',' });
                                entity[field.FieldName] = new OptionSetValueCollection(sRes.Select(t => new OptionSetValue(Int32.Parse(t.ToString()))).ToList());
                            }
                            else
                                entity[field.FieldName] = null;
                            break;
                        }
                    case Constants.FieldType.PartyList:
                        {
                            if (value.Value != null && value.Value.ToString().Length > 0)
                            {
                                entity[field.FieldName] = value.Value;
                                List<PartyListModel> lsPartyList = Extension.DeSerializeDictionary<List<PartyListModel>>(value.Value.ToString());
                                EntityCollection lsParty = new EntityCollection();
                                foreach (PartyListModel m in lsPartyList)
                                {
                                    Entity party = new Entity(Activityparty.LogicalName);
                                    party[Activityparty.Partyid] = new EntityReference(m.PartyLogicalName, Guid.Parse(m.PartyId));
                                    lsParty.Entities.Add(party);
                                }
                                entity[field.FieldName] = lsParty;
                            }
                            else
                                entity[field.FieldName] = null;
                            break;
                        }
                    case Constants.FieldType.Byte:
                        {


                            if (value.Value != null && value.Value.ToString().Length > 0)
                            {
                                byte[] bytes = System.Convert.FromBase64String(value.Value.ToString());
                                entity[field.FieldName] = bytes;
                            }
                            else
                                entity[field.FieldName] = null;
                            break;
                        }
                    default:
                        {
                            throw new InvalidPluginExecutionException(string.Format(Constants.ErrMessage.ResponFieldType, value.Value));
                        }

                }
            }
        }
        private EntityReferenceCollection ConvertListEntityReferenceCollection(List<EntityRef> list)
        {
            return new EntityReferenceCollection(list.Select(t => new EntityReference(t.LogicalName, new Guid(t.Id))).ToList());
        }

        private void GetAliasValue(ref FieldValue value, Entity entity, string fieldName, AliasedValue aliasedValue)
        {
            switch (aliasedValue.Value.GetType().Name)
            {
                case Constants.FieldType.Boolean:
                    {

                        value.Value = (bool)aliasedValue.Value;
                        break;
                    }
                case Constants.FieldType.PickList:
                    {
                        value.Value = ((OptionSetValue)aliasedValue.Value).Value;
                        value.Formated = entity.FormattedValues[fieldName];
                        break;
                    }
                case Constants.FieldType.String:
                case Constants.FieldType.Memo:
                    {
                        value.Value = aliasedValue.Value;
                        break;
                    }
                case Constants.FieldType.Decimal:
                    {
                        value.Value = aliasedValue.Value;
                        break;
                    }
                case Constants.FieldType.Int:
                case Constants.FieldType.Int32:
                case Constants.FieldType.Int64:
                    {
                        value.Value = aliasedValue.Value;
                        break;
                    }
                case Constants.FieldType.DateTime:
                    {
                        value.Value = aliasedValue.Value.ToString();
                        break;
                    }
                case Constants.FieldType.Money:
                    {
                        value.Value = ((Money)aliasedValue.Value).Value;
                        break;
                    }
                case Constants.FieldType.Lookup:
                    {
                        value.LogicalName = ((EntityReference)aliasedValue.Value).LogicalName;
                        value.Id = ((EntityReference)aliasedValue.Value).Id.ToString();
                        value.Value = ((EntityReference)aliasedValue.Value).Name;
                        break;
                    }
                case Constants.FieldType.Guid:
                    {
                        value.Value = ((Guid)aliasedValue.Value).ToString();
                        break;
                    }
                case Constants.FieldType.Byte:
                    {
                        value.Value = Convert.ToBase64String((byte[])aliasedValue.Value);
                        break;
                    }
                case Constants.FieldType.OptionSetValueCollection:
                    {
                        value.Value = string.Join(",", ((OptionSetValueCollection)aliasedValue.Value).Select(t => t.Value).ToList());
                        break;
                    }
            }
        }

        //private void CheckUser(IOrganizationService service, string userPortal, ref bool disabled, ref bool isRequiredLogout)
        //{
        //    if (string.IsNullOrEmpty(userPortal))
        //    {
        //        disabled = false;
        //        isRequiredLogout = false;
        //        return;
        //    }
        //    QueryExpression userDisQuery = new QueryExpression(RestrictedUser.LogicalName);
        //    userDisQuery.ColumnSet = new ColumnSet(RestrictedUser.Name, RestrictedUser.IsDisabled, RestrictedUser.IsLogOutDevice, RestrictedUser.User);
        //    userDisQuery.Criteria = new FilterExpression();
        //    userDisQuery.Criteria.AddCondition(new ConditionExpression(RestrictedUser.Name, ConditionOperator.Equal, userPortal));
        //    userDisQuery.TopCount = 1;
        //    Entity userDis = service.RetrieveMultiple(userDisQuery).Entities.FirstOrDefault();
        //    if (userDis != null)
        //    {
        //        if (userDis.Contains(RestrictedUser.IsDisabled) && (bool)userDis[RestrictedUser.IsDisabled])
        //            disabled = (bool)userDis[RestrictedUser.IsDisabled];
        //        else
        //            disabled = false;
        //        if (userDis.Contains(RestrictedUser.IsLogOutDevice) && (bool)userDis[RestrictedUser.IsLogOutDevice])
        //            isRequiredLogout = (bool)userDis[RestrictedUser.IsLogOutDevice];
        //        else
        //            isRequiredLogout = false;

        //    }
        //    else
        //    {
        //        disabled = false;
        //        isRequiredLogout = false;
        //    }
        //}
    }
}
