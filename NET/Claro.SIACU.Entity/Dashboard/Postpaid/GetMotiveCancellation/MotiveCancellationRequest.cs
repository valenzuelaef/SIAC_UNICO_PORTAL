using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetMotiveCancellation
{
    [DataContract]
    public class MotiveCancellationRequest : Claro.Entity.Request
    {
        [DataMember]
        public string ContractCode { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
    }
}
