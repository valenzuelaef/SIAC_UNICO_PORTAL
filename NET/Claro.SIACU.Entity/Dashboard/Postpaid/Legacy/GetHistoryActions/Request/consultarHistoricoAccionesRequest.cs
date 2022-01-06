using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Request
{
    public class consultarHistoricoAccionesRequest
    {
        public string msisdn { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}