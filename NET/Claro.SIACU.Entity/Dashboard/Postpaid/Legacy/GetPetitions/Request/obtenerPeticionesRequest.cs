using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPetitions.Conmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPetitions.Request
{
    public class obtenerPeticionesRequest
    {
        public string coId { get; set; }
        public string coIdPub { get; set; }
        public string migrado { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
