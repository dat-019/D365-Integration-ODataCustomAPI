using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace APIService.Provider.SharedLib
{
    [DataContract]
    public class RecordModel
    {
        [DataMember(Name = "logicalName")]
        public string LogicalName { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "field")]
        public Dictionary<string,FieldModel> Field { get; set; }


    }

    [DataContract]
    public class FieldModel
    {
        [DataMember(Name = "fieldName")]
        public string FieldName { get; set; }

        [DataMember(Name = "fieldType")]
        public string FieldType { get; set; }

        [DataMember(Name = "fieldValue")]
        public FieldValue FieldValue { get; set; }
    }

    [DataContract]
    public class FieldValue
    {
        [DataMember(Name = "logicalName")]
        public string LogicalName { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "value")]
        
        public object Value { get; set; }

        [DataMember(Name = "formated")]
        public object Formated { get; set; }

        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            var x = "";
        }
    }

    [DataContract]
    public class ExcuteNNRecord
    {
        [DataMember(Name = "logicalName")]
        public string LogicalName { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "relationShip")]
        public string RelationShip { get; set; }
        [DataMember(Name = "listEntity")]
        public List<EntityRef> ListEntity { get; set; }
    }
    [DataContract]
    public class EntityRef
    {
        [DataMember(Name = "logicalName")]
        public string LogicalName { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
    [DataContract]
    public class PageInfor
    {
        [DataMember(Name = "moreRecords")]
        public bool MoreRecords { get; set; }
        [DataMember(Name = "pagingCookie")]
        public string PagingCookie { get; set; }
        [DataMember(Name = "totalRecordCount")]
        public int TotalRecordCount { get; set; }
        [DataMember(Name = "results")]
        public List<RecordModel> Results { get; set; }
    }
}
