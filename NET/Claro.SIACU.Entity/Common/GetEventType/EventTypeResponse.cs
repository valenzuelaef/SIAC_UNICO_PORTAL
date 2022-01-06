using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetEventType
{
    [DataContract(Name = "EventTypeResponseCommon")]
    public class EventTypeResponse
    {
        [DataMember]
        public List<ListItem> EventTypes { get; set; }
    }
}
