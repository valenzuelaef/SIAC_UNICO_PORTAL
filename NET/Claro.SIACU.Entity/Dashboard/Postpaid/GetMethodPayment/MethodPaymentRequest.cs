using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetMethodPayment
{
    [DataContract(Name = "MethodPaymentRequestPostPaid")]
    public class MethodPaymentRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
    }
}
