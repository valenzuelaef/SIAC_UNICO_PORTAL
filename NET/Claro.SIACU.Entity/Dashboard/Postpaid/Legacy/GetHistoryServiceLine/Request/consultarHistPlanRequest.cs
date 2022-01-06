using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Request
{
    public class consultarHistPlanRequest
    {
        public string coId { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
