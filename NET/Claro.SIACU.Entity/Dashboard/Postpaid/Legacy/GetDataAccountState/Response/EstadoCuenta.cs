using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Response
{
    public class EstadoCuenta
    {
        public string nombreCliente { get; set; }
        public string deudaActual { get; set; }
        public string deudaVencida { get; set; }
        public string totalMontoDisputa { get; set; }
        public string fechaUltFactura { get; set; }
        public string fechaUltPago { get; set; }
        public string codCuenta { get; set; }
        public string codCuentaAlterna { get; set; }
        public string descripUbigeo { get; set; }
        public string tipoCliente { get; set; }
        public string estadoCuenta { get; set; }
        public string fechaActivacion { get; set; }
        public string cicloFacturacion { get; set; }
        public string limiteCredito { get; set; }
        public string creditScore { get; set; }
        public string tipoPago { get; set; }
        public List<DetalleEecc> listaDetalleEecc { get; set; }
        public List<DetalleDist> listaDetalleDist { get; set; }
    }
}
