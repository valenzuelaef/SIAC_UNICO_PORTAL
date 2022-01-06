using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccountDP
{
    public class StatusAccountDPMessageResponse
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersResponse Header { get; set; }

        [DataMember( Name = "Body")]
        public StatusAccountDPBodyResponse Body { get; set; }
    }
}
