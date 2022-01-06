using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetMethodPayment
{
    [DataContract(Name = "MethodPaymentResponsePostPaid")]
    public class MethodPaymentResponse
    {
        [DataMember]
        public Entity.Dashboard.Postpaid.MethodPayment ObjMethodPayment { get; set; }
    }
}
