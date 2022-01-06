using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Response
{
    public class ResponseData
    {
        public List<ListaHistoricoAnotacione> listaHistoricoAnotaciones { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
