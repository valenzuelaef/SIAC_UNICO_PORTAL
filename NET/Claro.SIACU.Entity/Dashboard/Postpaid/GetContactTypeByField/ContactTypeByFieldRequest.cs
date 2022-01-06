using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetContactTypeByField
{
    [DataContract(Name = "ContactTypeByFieldRequestPostPaid")]
    public class ContactTypeByFieldRequest : Claro.Entity.Request
    {
        [DataMember]
        public int Code { get; set; } 
    }
}
