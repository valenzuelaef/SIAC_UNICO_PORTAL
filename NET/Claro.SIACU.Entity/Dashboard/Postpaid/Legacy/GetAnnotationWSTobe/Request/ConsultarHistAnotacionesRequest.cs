using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Request
{
    public class ConsultarHistAnotacionesRequest
    {
        public string coId { get; set; }
        public string csType { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
