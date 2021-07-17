using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIConsumer
{
    /// <summary>
    /// Ref - Token Based Authentication in Web API: 
    /// https://dotnettutorials.net/lesson/token-based-authentication-web-api/
    //  https://www.c-sharpcorner.com/article/calling-web-api-using-httpclient/
    /// </summary>
    public class ConsumeCustomAPI : IConsumeCustomAPI
    {
        private string _apiAccessToken;
        public ConsumeCustomAPI(string apiAccessToken)
        {
            this._apiAccessToken = apiAccessToken;
        }

        public void ConsumeFetchCustomAPI()
        {
            var stringModel = new StringModel();
            stringModel.Data = $@"
                                <fetch top='50'>
                                  <entity name='c30seeds_nhanvien'>
                                    <attribute name='c30seeds_nhanvienid' />
                                    <attribute name='c30seeds_marosy' />
                                    <attribute name='c30seeds_sodt' />
                                    <attribute name='c30seeds_sdthotro' />
                                    <attribute name='c30seeds_taikhoan' />
                                    <attribute name='c30seeds_san' />
                                    <attribute name='c30seeds_dongbo' />
                                    <attribute name='c30seeds_cmnd' />
                                    <attribute name='c30seeds_email' />
                                    <attribute name='c30seeds_phophongkd' />
                                    <attribute name='c30seeds_ngaysinh' />
                                    <attribute name='c30seeds_manv' />
                                    <attribute name='c30seeds_password' />
                                    <attribute name='c30seeds_diachi' />
                                    <attribute name='c30seeds_phongkd' />
                                    <attribute name='c30seeds_systemuser' />
                                    <attribute name='c30seeds_name' />
                                    <attribute name='c30seeds_lydo' />
                                    <attribute name='c30seeds_oldpassword' />
                                    <attribute name='c30seeds_nhom' />
                                    <filter>
                                      <condition attribute='statecode' operator='eq' value='0'/>
                                      <filter type='or'>
                                        <condition attribute='c30seeds_san' operator='eq-businessid' />
                                        <condition attribute='c30seeds_phongkd' operator='eq-businessid' />
                                      </filter>
                                    </filter>
                                  </entity>
                                </fetch>";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44333/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(Accept.Json));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + this._apiAccessToken);

                var responseObj = client.PostAsJsonAsync<StringModel>("api/crm/fetch", stringModel).Result; //Calling the OData Api

                if (responseObj.IsSuccessStatusCode)
                {
                    JObject returnedObj = responseObj.Content.ReadAsStringAsync().Result != null ? JsonConvert.DeserializeObject<JObject>(responseObj.Content.ReadAsStringAsync().Result) : null;
                    //returnedObj = { { "@odata.context": "https://kogsandbox.crm5.dynamics.com/api/data/v9.0/$metadata#Microsoft.Dynamics.CRM.cr01f_DynamicServicesResponse",  "Response": "{\"moreRecords\":false,\"pagingCookie\":\"\",\"results\":[{\"field\":{\"c30seeds_manv\":{\"fieldName\":\"c30seeds_manv\",\"fieldType\":\"String\",\"fieldValue\":{\"formated\":null,\"id\":null,\"logicalName\":null,\"value\":\"NV01\"}},\"c30seeds_email\":{\"fieldName\":\"c30seeds_email\",\"fieldType\":\"String\",\"fieldValue\":{\"formated\":null,\"id\":null,\"logicalName\":null,\"value\":\"NV01\"}},\"c30seeds_dongbo\":{\"fieldName\":\"c30seeds_dongbo\",\"fieldType\":\"Boolean\",\"fieldValue\":{\"formated\":\"Có\",\"id\":null,\"logicalName\":null,\"value\":true}},\"c30seeds_systemuser\":{\"fieldName\":\"c30seeds_systemuser\",\"fieldType\":\"EntityReference\",\"fieldValue\":{\"formated\":null,\"id\":\"9400413e-d57e-ea11-a811-000d3aa3e061\",\"logicalName\":\"systemuser\",\"value\":\"Ban Tổng Giám Đốc #\"}},\"c30seeds_nhanvienid\":{\"fieldName\":\"c30seeds_nhanvienid\",\"fieldType\":\"Guid\",\"fieldValue\":{\"formated\":null,\"id\":null,\"logicalName\":null,\"value\":\"fd9bcaf2-8e54-eb11-a812-002248565d93\"}},\"c30seeds_phongkd\":{\"fieldName\":\"c30seeds_phongkd\",\"fieldType\":\"EntityReference\",\"fieldValue\":{\"formated\":null,\"id\":\"23011668-e97f-ea11-a811-000d3aa3e47c\",\"logicalName\":\"businessunit\",\"value\":\"Phòng 1 (SKD 1)\"}},\"c30seeds_nhom\":{\"fieldName\":\"c30seeds_nhom\",\"fieldType\":\"OptionSetValue\",\"fieldValue\":{\"formated\":\"Nhóm 4\",\"id\":null,\"logicalName\":null,\"value\":100000003}},\"c30seeds_name\":{\"fieldName\":\"c30seeds_name\",\"fieldType\":\"String\",\"fieldValue\":{\"formated\":null,\"id\":null,\"logicalName\":null,\"value\":\"NV01\"}},\"c30seeds_taikhoan\":{\"fieldName\":\"c30seeds_taikhoan\",\"fieldType\":\"String\",\"fieldValue\":{\"formated\":null,\"id\":null,\"logicalName\":null,\"value\":\"01\"}},\"c30seeds_san\":{\"fieldName\":\"c30seeds_san\",\"fieldType\":\"EntityReference\",\"fieldValue\":{\"formated\":null,\"id\":\"558f3d45-667c-ea11-a811-000d3aa2f915\",\"logicalName\":\"businessunit\",\"value\":\"KOG\"}}},\"id\":\"fd9bcaf2-8e54-eb11-a812-002248565d93\",\"logicalName\":\"c30seeds_nhanvien\"},{\"field\":{\"c30seeds_taikhoan\":{\"fieldName\":\"c30seeds_taikhoan\",\"fieldType\":\"String\",\"fieldValue\":{\"formated\":null,\"id\":null,\"logicalName\":null,\"value\":\"tgd@diaockimoanh.com.vn\"}},\"c30seeds_phongkd\":{\"fieldName\":\"c30seeds_phongkd\",\"fieldType\":\"EntityReference\",\"fieldValue\":{\"formated\":null,\"id\":\"558f3d45-667c-ea11-a811-000d3aa2f915\",\"logicalName\":\"businessunit\",\"value\":\"KOG\"}},\"c30seeds_name\":{\"fieldName\":\"c30seeds_name\",\"fieldType\":\"String\",\"fieldValue\":{\"formated\":null,\"id\":null,\"logicalName\":null,\"value\":\"tgd\"}},\"c30seeds_nhanvienid\":{\"fieldName\":\"c30seeds_nhanvienid\",\"fieldType\":\"Guid\",\"fieldValue\":{\"formated\":null,\"id\":null,\"logicalName\":null,\"value\":\"4b6d0bd3-1de2-eb11-bacb-00224857a857\"}},\"c30seeds_email\":{\"fieldName\":\"c30seeds_email\",\"fieldType\":\"String\",\"fieldValue\":{\"formated\":null,\"id\":null,\"logicalName\":null,\"value\":\"tgd@diaockimoanh.com.vn\"}},\"c30seeds_nhom\":{\"fieldName\":\"c30seeds_nhom\",\"fieldType\":\"OptionSetValue\",\"fieldValue\":{\"formated\":\"Nhóm 1\",\"id\":null,\"logicalName\":null,\"value\":100000000}},\"c30seeds_san\":{\"fieldName\":\"c30seeds_san\",\"fieldType\":\"EntityReference\",\"fieldValue\":{\"formated\":null,\"id\":\"558f3d45-667c-ea11-a811-000d3aa2f915\",\"logicalName\":\"businessunit\",\"value\":\"KOG\"}},\"c30seeds_dongbo\":{\"fieldName\":\"c30seeds_dongbo\",\"fieldType\":\"Boolean\",\"fieldValue\":{\"formated\":\"Có\",\"id\":null,\"logicalName\":null,\"value\":true}},\"c30seeds_systemuser\":{\"fieldName\":\"c30seeds_systemuser\",\"fieldType\":\"EntityReference\",\"fieldValue\":{\"formated\":null,\"id\":\"d52fd261-a1b4-ea11-a812-000d3aa3ec6b\",\"logicalName\":\"systemuser\",\"value\":\"# Dynamics365Athena2\"}}},\"id\":\"4b6d0bd3-1de2-eb11-bacb-00224857a857\",\"logicalName\":\"c30seeds_nhanvien\"}],\"totalRecordCount\":-1}"} }

                    if (returnedObj == null) return;

                    var response = returnedObj["Response"].ToString(); // "Response"

                    PageInfor pageInfor = Extension.DeSerializeObject<PageInfor>(response);
                    List<RecordModel> records = pageInfor.Results;

                    if (records == null || records.Count() == 0)
                        return;

                    int count = 0;
                    foreach (var record in records)
                    {
                        count += 1;
                        Console.WriteLine("Record ID " + count + ":: " + record.Id);
                    }
                }
            }
        }

        public void ConsumeCreateRecordCustomAPI()
        {
            RecordModel recordModel = CreateNewRecord();
            StringModel stringModel = new StringModel();
            stringModel.Data = Extension.SerializeObject<RecordModel>(recordModel);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44333/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + this._apiAccessToken);

                var responseObj = client.PostAsJsonAsync<StringModel>("api/crm/create", stringModel).Result; //Calling the OData Api
                if (responseObj.IsSuccessStatusCode)
                {
                    Console.WriteLine("Record created!");
                }

            }

        }

        private RecordModel CreateNewRecord()
        {
            RecordModel newRec = new RecordModel();
            newRec.LogicalName = "contact"; //create new contact
            //newRec.Fields = new Dictionary<string, FieldModel>();
            newRec.Fields = new List<FieldModel>();

            //fields
            FieldModel fieldModel = new FieldModel();
            fieldModel.FieldType = FieldType.String;
            fieldModel.FieldName = "yomifirstname";
            fieldModel.FieldValue = new FieldValue
            {
                LogicalName = "yomifirstname",
                Value = "dat019-testing"
            };
            newRec.Fields.Add(fieldModel);


            fieldModel = new FieldModel();
            fieldModel.FieldType = FieldType.String;
            fieldModel.FieldName = "c30seeds_sodienthoait2";
            fieldModel.FieldValue = new FieldValue
            {
                LogicalName = "c30seeds_sodienthoait2",
                Value = "123456789"
            };
            newRec.Fields.Add(fieldModel);


            return newRec;
        }
    }
}
