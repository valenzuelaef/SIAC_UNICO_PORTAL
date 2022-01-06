using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetPaymentCollection
{
    [DataContract(Name = "PaymentCollectionResponsePostPaid")]
    public class PaymentCollectionResponse
    {
        [DataMember]
        public Entity.Dashboard.Postpaid.PaymentCollection ObjPaymentCollection { get; set; }
    } 
}
