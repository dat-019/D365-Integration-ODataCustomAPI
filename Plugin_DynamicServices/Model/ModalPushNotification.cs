using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Plugin_DynamicServices
{
    [DataContract]
    public class ModalPushNotification
    {
        [DataMember(Name = "data")]
        public DataNoti Data { get; set; }

        [DataMember(Name = "arrayUser")]
        public List<string> ArrayUser { get; set; }
    }

    [DataContract]
    public class DataNoti
    {

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "logicalName")]
        public string LogicalName { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }
    }


    [DataContract]
    public class DataNotification
    {

        [DataMember]
        public string priority { get; set; }

        [DataMember]
        public bool content_available { get; set; }

        [DataMember]
        public Notification notification { get; set; }

        [DataMember]
        public bool badge { get; set; }

        [DataMember]
        public string sound { get; set; }

        [DataMember]
        public string condition { get; set; }

        [DataMember]
        public Params data { get; set; }
    }


    [DataContract]
    public class Notification
    {

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public string body { get; set; }
    }

    [DataContract]
    public class Params
    {

        [DataMember]
        public string logicalName { get; set; }

        [DataMember]
        public string id { get; set; }
    }
}
