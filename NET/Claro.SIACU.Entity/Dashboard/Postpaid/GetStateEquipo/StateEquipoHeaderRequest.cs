using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStateEquipo
{
    public class StateEquipoHeaderRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public Common.GetDataPower.HeaderRequest HeaderRequest { get; set; }
    }
}
