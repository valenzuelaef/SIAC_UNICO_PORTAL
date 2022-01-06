using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.EvaluateAmount
{
    public class EvaluateAmountResponse
    {
        [DataMember(Name = "MessageResponse")]
        public EvaluateAmountMessageResponse MessageResponse { get; set; }
    }
}
