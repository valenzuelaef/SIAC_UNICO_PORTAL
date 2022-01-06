using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataHistory.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataHistory.Request
{
    public class ConsultarHistoricoDatosRequest
    {
        public string customerId { get; set; }
        public listaOpcional listaOpcional { get; set; }
    }
}
