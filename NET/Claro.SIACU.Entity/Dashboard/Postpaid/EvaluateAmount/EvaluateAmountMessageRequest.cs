using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.EvaluateAmount
{
    public class EvaluateAmountMessageRequest
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest Header { get; set; }

        [DataMember(Name = "Body")]
        public EvaluateAmountBodyRequest Body { get; set; }
    }
}
