using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave
{
    [DataContract(Name = "ContactSaveResponsePostPaid")]
    public class ContactSaveResponse
    {
        [DataMember]
        public string ResponseCode { get; set; }
        [DataMember]
        public string ResponseMessage { get; set; }
    }
}
