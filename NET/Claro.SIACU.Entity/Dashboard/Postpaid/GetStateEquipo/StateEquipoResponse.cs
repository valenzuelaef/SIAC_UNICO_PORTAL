using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStateEquipo
{
    
    public class StateEquipoResponse
    {
         [DataMember(Name = "MessageResponse")]
         public StateEquipoMessageResponse MessageResponse { get; set; }
    }
}
