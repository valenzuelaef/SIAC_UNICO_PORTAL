using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetListHistoryIMSI.Common;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetListHistoryIMSI.Response
{
    public class responseData
    {
        public List<listaHistoricoDato> listaHistoricoDatos { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
