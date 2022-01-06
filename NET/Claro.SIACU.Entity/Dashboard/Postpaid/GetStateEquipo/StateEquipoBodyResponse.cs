using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStateEquipo
{
    
    public class StateEquipoBodyResponse
    {
        [DataMember(Name = "PO_ESTADO")]
        public string PO_ESTADO { get; set; }

        [DataMember(Name = "PO_CODERROR")]
        public string PO_CODERROR { get; set; }

         [DataMember(Name = "PO_MSJERROR")]
        public string PO_MSJERROR { get; set; } 
            
    }
}
