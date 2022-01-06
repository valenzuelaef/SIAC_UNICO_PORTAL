using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStateEquipo
{
    public class StateEquipoMessageRequest
    {
        [DataMember(Name = "Header")]
        public StateEquipoHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public StateEquipoBodyRequest Body { get; set; }

    }
}
