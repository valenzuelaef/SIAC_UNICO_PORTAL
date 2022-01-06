using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Common;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Request
{
   public class ConsultarHistoricoPaquetesRequest
    {
        public string numeroLinea { get; set; }
        public string coIdPub { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string tipoUnidad { get; set; }
        public List<ListaOpcional> ListaOpcional { get; set; }
    }
}
