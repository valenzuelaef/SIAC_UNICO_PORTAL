using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.EvaluateAmount
{
    public class EvaluateAmountRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public EvaluateAmountMessageRequest MessageRequest { get; set; }
    }
}
