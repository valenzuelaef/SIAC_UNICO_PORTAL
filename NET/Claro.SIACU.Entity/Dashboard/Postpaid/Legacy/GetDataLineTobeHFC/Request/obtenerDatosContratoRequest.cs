using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Request
{
    public class obtenerDatosContratoRequest
    {
        public string coId { get; set; }

        public List<listaOpcional> listaOpcional { get; set; }
    }
}
