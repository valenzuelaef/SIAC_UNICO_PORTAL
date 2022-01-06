using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetPaymentCollection
{
    [DataContract(Name = "PaymentCollectionRequestPostPaid")]
    public class PaymentCollectionRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string csIdPub { get; set; }
    }
}
