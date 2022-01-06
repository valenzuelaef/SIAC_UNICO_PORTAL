using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Response
{
    public class ListaHistoricoPaquete
    {
        public string nombrePaquete { get; set; }
        public string vigencia { get; set; }
        public string tipoVigencia { get; set; }
        public string fechaVencimiento { get; set; }
        public string costo { get; set; }
        public string tipoCompra { get; set; }
        public string fechaAdquisicion { get; set; }
        public string estado { get; set; }
        public string saldo { get; set; }
        public string tipoPaquete { get; set; }
    }
}
