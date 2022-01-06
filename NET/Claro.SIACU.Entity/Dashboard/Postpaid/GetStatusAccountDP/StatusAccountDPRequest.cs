using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccountDP
{
    public class StatusAccountDPRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")] 
        public StatusAccountDPMessageRequest MessageRequest { get; set; }
    }
}
