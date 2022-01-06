using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLine.Request
{
    public class obtenerDatosContratoRequest
    {
        public string coId { get; set; }

        public List<listaOpcional> listaOpcional { get; set; }
    }
}
