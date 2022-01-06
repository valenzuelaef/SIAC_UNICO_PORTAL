using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Response
{
    public class responseData
    {
        public List<Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Response.listaHistoricoAcciones> listaHistoricoAcciones { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}