using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Response
{
    public class DetalleEecc
    {
        public string tipoDocumento { get; set; }
        public string nroDocumento { get; set; }
        public string descripDocumento { get; set; }
        public string estadoDocumento { get; set; }
        public string fechaRegistro { get; set; }
        public string fechaEmision { get; set; }
        public string fechaVencimiento { get; set; }
        public string tipoMoneda { get; set; }
        public string montoDocumento { get; set; }
        public string montoFco { get; set; }
        public string montoFinan { get; set; }
        public string saldoDocumento { get; set; }
        public string saldoFco { get; set; }
        public string saldoFinan { get; set; }
        public string montoSoles { get; set; }
        public string montoDolares { get; set; }
        public string cargo { get; set; }
        public string abono { get; set; }
        public string saldoCuenta { get; set; }
        public string nroOperacionPago { get; set; }
        public string fechaPago { get; set; }
        public string formaPago { get; set; }
        public string anioEmision { get; set; }
        public string mesEmision { get; set; }
        public string anioVencimiento { get; set; }
        public string mesVencimiento { get; set; }
        public string flagCargoCuenta { get; set; }
        public string nroTicket { get; set; }
        public string montoReclamado { get; set; }
        public string telefono { get; set; }
        public string usuario { get; set; }
        public string idDocSistemaOrig  { get; set; }
        public string descripExtend { get; set; }
        public string idDocOac { get; set; }
        public string motivoAjuste { get; set; }
        public string descripcionAjuste { get; set; }
        public string idOnbase  { get; set; }
    }
}
