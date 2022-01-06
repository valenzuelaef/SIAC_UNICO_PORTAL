using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStateEquipo
{
   public class StateEquipoMessageResponse
    {
        [DataMember(Name = "Header")]
       public StateEquipoHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public StateEquipoBodyResponse Body { get; set; }

    }
}
