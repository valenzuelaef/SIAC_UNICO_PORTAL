using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetPaymentCommitment
{
    [DataContract(Name = "PaymentCommitmentRequestPostPaid")]
    public class PaymentCommitmentRequest : Claro.Entity.Request
    {

        [DataMember]
        public string IdCustomer { get; set; }
    }
}
