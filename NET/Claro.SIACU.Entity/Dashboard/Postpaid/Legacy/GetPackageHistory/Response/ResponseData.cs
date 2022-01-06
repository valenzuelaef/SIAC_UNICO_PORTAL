using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Common;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Response
{
    public class ResponseData
    {
        public List<ListaHistoricoPaquete> listaHistoricoPaquete { get; set; }
        public List<ListaOpcional> ListaOpcional { get; set; }
    }
}
