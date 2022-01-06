using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPetitions.Conmon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPetitions.Response
{
    public class obtenerPeticionesResponseType
    {
        public responseAudit auditResponse { get; set; }
        public List<ListaPeticiones> listaPeticiones { get; set; }
        public List<listaOpcional> listaCamposAdicionales { get; set; }
    }
}
