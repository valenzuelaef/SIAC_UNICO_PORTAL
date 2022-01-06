using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Response
{
    public class responseData
    {
        public List<listaHistPlan> listaHistPlan { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
