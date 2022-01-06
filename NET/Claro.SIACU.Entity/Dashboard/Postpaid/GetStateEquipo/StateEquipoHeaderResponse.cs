using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStateEquipo
{
    public class StateEquipoHeaderResponse
    {
        [DataMember(Name = "HeaderResponse")]
        public Common.GetDataPower.HeaderResponse HeaderResponse { get; set; }

    }
}
