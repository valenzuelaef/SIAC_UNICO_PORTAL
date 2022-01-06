using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccountDP
{
    public class StatusAccountDPBodyResponse
    {
        [DataMember(Name = "idTransaccion")]
        public string idTransaccion { get; set; }

        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }

        [DataMember(Name = "mensajeRespuesta")]
        public string mensajeRespuesta { get; set; }

        [DataMember(Name = "estadoCuenta")]
        public estadoCuenta estadoCuenta { get; set; }
    }

    public class estadoCuenta
    {
        public IList<detalleEstadoCuentaCab> detalleEstadoCuentaCab { get; set; }
    } 

    public class detalleEstadoCuentaCab
    {
        [DataMember(Name = "nombreCliente")]
        public string nombreCliente { get; set; }

        [DataMember(Name = "deudaActual")]
        public decimal deudaActual { get; set; }

        [DataMember(Name = "deudaVencida")]
        public decimal deudaVencida { get; set; }

        [DataMember(Name = "totalMontoDisputa")]
        public decimal totalMontoDisputa { get; set; }

        [DataMember(Name = "fechaUltimaFactura")]
        public string fechaUltimaFactura { get; set; }

        [DataMember(Name = "fechaUltimaPago")]
        public string fechaUltimaPago { get; set; }

        [DataMember(Name = "codigoCuenta")]
        public string codigoCuenta { get; set; }

        [DataMember(Name = "codigoCuentaAlterna")]
        public string codigoCuentaAlterna { get; set; }

        [DataMember(Name = "descUbigeo")]
        public string descUbigeo { get; set; }

        [DataMember(Name = "tipoCliente")]
        public string tipoCliente { get; set; }

        [DataMember(Name = "estadoCuenta")]
        public string estadoCuenta { get; set; }

        [DataMember(Name = "fechaActivacion")]
        public string fechaActivacion { get; set; }

        [DataMember(Name = "cicloFacturacion")]
        public string cicloFacturacion { get; set; }

        [DataMember(Name = "limiteCredito")]
        public decimal limiteCredito { get; set; }

        [DataMember(Name = "creditScore")]
        public string creditScore { get; set; }

        [DataMember(Name = "tipoPago")]
        public string tipoPago { get; set; }

        [DataMember(Name = "detalle")]
        public detalle detalle { get; set; }
    }

    public class detalle
    {
        [DataMember(Name = "detalleEstadoCuenta")]
        public IList<detalleEstadoCuenta> detalleEstadoCuenta { get; set; }
    }

    public class detalleEstadoCuenta
    {
        [DataMember(Name = "tipoDocumento")]
        public string tipoDocumento { get; set; }

        [DataMember(Name = "nroDocumento")]
        public string nroDocumento { get; set; }

        [DataMember(Name = "descripcionDocumento")]
        public string descripcionDocumento { get; set; }

        [DataMember(Name = "estadoDocumento")]
        public string estadoDocumento { get; set; }

        [DataMember(Name = "fechaRegistro")]
        public string fechaRegistro { get; set; }

        [DataMember(Name = "fechaEmision")]
        public string fechaEmision { get; set; }

        [DataMember(Name = "fechaVencimiento")]
        public string fechaVencimiento { get; set; }

        [DataMember(Name = "tipoMoneda")]
        public string tipoMoneda { get; set; }

        [DataMember(Name = "montoDocumento")]
        public decimal montoDocumento { get; set; }

        [DataMember(Name = "montoFco")]
        public decimal montoFco { get; set; }

        [DataMember(Name = "montoFinan")]
        public decimal montoFinan { get; set; }

        [DataMember(Name = "saldoDocumento")]
        public decimal saldoDocumento { get; set; }

        [DataMember(Name = "saldoFco")]
        public decimal saldoFco { get; set; }

        [DataMember(Name = "saldoFinan")]
        public decimal saldoFinan { get; set; }

        [DataMember(Name = "montoSoles")]
        public decimal montoSoles { get; set; }
        
        [DataMember(Name = "montoDolares")]
        public decimal montoDolares { get; set; }

        [DataMember(Name = "cargo")]
        public decimal cargo { get; set; }

        [DataMember(Name = "abono")]
        public decimal abono { get; set; }

        [DataMember(Name = "saldoCuenta")]
        public decimal saldoCuenta { get; set; }

        [DataMember(Name = "nroOperacionPago")]
        public string nroOperacionPago { get; set; }

        [DataMember(Name = "fechaPago")]
        public string fechaPago { get; set; }

        [DataMember(Name = "formaPago")]
        public string formaPago { get; set; }

        [DataMember(Name = "docAnio")]
        public decimal docAnio { get; set; }

        [DataMember(Name = "docMes")]
        public decimal docMes { get; set; }
        
        [DataMember(Name = "docAnioVenc")]
        public decimal docAnioVenc { get; set; }

        [DataMember(Name = "docMesVenc")]
        public decimal docMesVenc { get; set; }

        [DataMember(Name = "flagCargoCta")]
        public string flagCargoCta { get; set; }

        [DataMember(Name = "nroTicket")]
        public string nroTicket { get; set; }

        [DataMember(Name = "montoReclamado")]
        public decimal montoReclamado { get; set; }

        [DataMember(Name = "telefono")]
        public string telefono { get; set; }

        [DataMember(Name = "usuario")]
        public string usuario { get; set; }

        [DataMember(Name = "idDocumentoOrigen")]
        public string idDocumentoOrigen { get; set; }

        [DataMember(Name = "descripcionExtend")]
        public string descripcionExtend { get; set; }

        [DataMember(Name = "idDocOAC")]
        public decimal idDocOAC { get; set; }

        [DataMember(Name = "flagBotonAnulacion")]
        public string flagBotonAnulacion { get; set; }
    }
}
