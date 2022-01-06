using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.EvaluateAmount
{
    public class EvaluateAmountMessageResponse
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersResponse Header { get; set; }

        [DataMember(Name = "Body")]
        public EvaluateAmountBodyResponse Body { get; set; }
    }
}
