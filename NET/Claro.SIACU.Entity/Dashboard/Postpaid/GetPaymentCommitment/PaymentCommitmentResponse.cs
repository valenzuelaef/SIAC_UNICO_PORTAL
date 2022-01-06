using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetPaymentCommitment
{
    [DataContract(Name = "PaymentCommitmentResponsePostPaid")]
    public class PaymentCommitmentResponse
    {
        [DataMember]
        public List<Entity.Dashboard.Postpaid.Account> ListAccount { get; set; }


    }
}
