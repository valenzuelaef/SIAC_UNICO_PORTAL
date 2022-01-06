using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccountDP
{
    public class StatusAccountDPMessageRequest
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest Header { get; set; }

        [DataMember(Name = "Body")]
        public StatusAccountDPBodyRequest Body { get; set; }
    }
}
