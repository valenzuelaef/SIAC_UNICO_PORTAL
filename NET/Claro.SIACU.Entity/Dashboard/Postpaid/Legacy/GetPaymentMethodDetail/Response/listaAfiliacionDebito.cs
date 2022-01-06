using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Response
{
    public class listaAfiliacionDebito
    {
        public string desEstadoSolicitud { get; set; }
        public string tipoDebito { get; set; }
        public string numeroCuentaTarjeta { get; set; }
        public string fechaExpiracionTarjeta { get; set; }
        public string descTipoCuenta { get; set; }
        public string descMoneda { get; set; }
        public string descripcionLarga { get; set; }
        public string montoMaximo { get; set; }
        public string fechaRegistro { get; set; }
        public string fechaAprobado { get; set; }
        public string fechaDesafiliacion { get; set; }
        public string motivo { get; set; }
        public string numeroSolicitud { get; set; }
        public string fechaRechazo { get; set; }
        public string motivoSolicitud { get; set; }
        public string codEntidad { get; set; }
        public string dniTitular { get; set; }
        public string nombreTitular { get; set; }
        public string fechaProceso { get; set; }
        public string tipoCuentaBancaria { get; set; }
        public string moneda { get; set; }
    }
}
