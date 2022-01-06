using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetEventType
{
    [DataContract(Name = "EventTypeRequestCommon")]
    public class EventTypeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string EventType { get; set; }
    }
}
