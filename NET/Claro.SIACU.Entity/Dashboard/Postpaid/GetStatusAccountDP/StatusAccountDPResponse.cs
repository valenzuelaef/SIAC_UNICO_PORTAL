using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccountDP
{
    public class StatusAccountDPResponse
    {
        [DataMember(Name = "MessageResponse")]
        public StatusAccountDPMessageResponse MessageResponse { get; set; }
    }
}
