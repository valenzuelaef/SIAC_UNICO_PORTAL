using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetAffiliationToDebit
{
    [DataContract(Name = "AffiliationToDebitRequestPostPaid")]
    public class AffiliationToDebitRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string flagPlataforma { get; set; }
        [DataMember]
        public string csIdPub { get; set; }
    }
}
