using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStateEquipo
{  
    public class StateEquipoRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public StateEquipoMessageRequest MessageRequest { get; set; }
    }
}
