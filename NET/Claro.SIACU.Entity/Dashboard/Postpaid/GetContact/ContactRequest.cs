using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetContact
{
    [DataContract(Name = "ContactRequestPostPaid")]
    public class ContactRequest : Claro.Entity.Request
    {
        [DataMember]
        public string DocumentCode { get; set; }
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public int  CustomerID { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public int SolinCode { get; set; }
        [DataMember]
        public int CSCTNCode { get; set; } 
    }
}
