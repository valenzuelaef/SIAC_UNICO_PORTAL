using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave
{
    [DataContract(Name = "ContactSaveRequestPostPaid")]
    public class ContactSaveRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string Save { get; set; }
        [DataMember]
        public string Delete { get; set; }
        [DataMember]
        public string ResponseCode { get; set; }
    }
}
