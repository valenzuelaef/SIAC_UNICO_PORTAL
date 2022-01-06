using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStateEquipo
{
      public class StateEquipoBodyRequest
    {
        [DataMember(Name = "PI_LINEA")]
        public string PI_LINEA { get; set; }
    }
}
