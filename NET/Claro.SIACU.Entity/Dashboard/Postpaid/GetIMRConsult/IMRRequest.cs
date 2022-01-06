using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetIMRConsult
{
    [DataContract(Name = "IMRRequestPostPaid")]
    public class IMRRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Msisdn { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public int BillingCycle { get; set; }
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public string CustomerType { get; set; }
        [DataMember]
        public string Segment { get; set; }
        [DataMember]
        public string ContractId { get; set; }
        [DataMember]
        public string IMRTotalAmount { get; set; }
        [DataMember]
        public string IMRAmount { get; set; }
        [DataMember]
        public string ExpirationDate { get; set; }
    }
}
