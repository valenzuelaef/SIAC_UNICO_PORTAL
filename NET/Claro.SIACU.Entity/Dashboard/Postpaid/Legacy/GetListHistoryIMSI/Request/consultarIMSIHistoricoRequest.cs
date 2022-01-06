using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetListHistoryIMSI.Common;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetListHistoryIMSI.Request
{
    public class consultarIMSIHistoricoRequest
    {
        public string coid { get; set; }
        public string numeroLinea { get; set; }
        public string plataforma { get; set; }
        public string fechaMigracion { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
