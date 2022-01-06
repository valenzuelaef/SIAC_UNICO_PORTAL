using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount
{
    [DataContract(Name = "xDetalleEstadoCuenta")]
    public class DetailStateAccount
    {
        [DataMember(Name = "xTipoDocumento")]
        public string typeDocument { get; set; }

        [DataMember(Name = "xNroDocumento")]
        public string nroDocument { get; set; }

        [DataMember(Name = "xDescripDocumento")]
        public string descripDocument { get; set; }

        [DataMember(Name = "xEstadoDocumento")]
        public string stateDocument { get; set; }

        [DataMember(Name = "xFechaRegistro")]
        public string registrationDate { get; set; }

        [DataMember(Name = "xFechaEmision")]
        public string emissionDate { get; set; }

        [DataMember(Name = "xFechaVencimiento")]
        public string expirationDate { get; set; }

        [DataMember(Name = "xTipoMoneda")]
        public string typeMoney { get; set; }

        [DataMember(Name = "xMontoDocumento")]
        public string amountDocument { get; set; }

        [DataMember(Name = "xMontoFco")]
        public string amountFco { get; set; }

        [DataMember(Name = "xMontoFinan")]
        public string amountFinan { get; set; }

        [DataMember(Name = "xSaldoDocumento")]
        public string balanceDocument { get; set; }

        [DataMember(Name = "xSaldoFco")]
        public string balanceFco { get; set; }

        [DataMember(Name = "xSaldoFinan")]
        public string balanceFinan { get; set; }

        [DataMember(Name = "xMontoSoles")]
        public string amountPEN { get; set; }

        [DataMember(Name = "xMontoDolares")]
        public string amountUSD { get; set; }

        [DataMember(Name = "xCargo")]
        public string charge { get; set; }

        [DataMember(Name = "xAbono")]
        public string payment { get; set; }

        [DataMember(Name = "xSaldoCuenta")]
        public string balanceAccount { get; set; }

        [DataMember(Name = "xNroOperacionPago")]
        public string numOperationPay { get; set; }

        [DataMember(Name = "xFechaPago")]
        public string datePay { get; set; }

        [DataMember(Name = "xFormaPago")]
        public string wayPay { get; set; }

        [DataMember(Name = "xDocAnio")]
        public string docYear { get; set; }

        [DataMember(Name = "xDocMes")]
        public string docMonth { get; set; }

        [DataMember(Name = "xDocAnioVenc")]
        public string docYearExpired { get; set; }

        [DataMember(Name = "xDocMesVenc")]
        public string docMonthExpired { get; set; }

        [DataMember(Name = "xFlagCargoCta")]
        public string flagChargeCta { get; set; }

        [DataMember(Name = "xNroTicket")]
        public string nroTicket { get; set; }

        [DataMember(Name = "xMontoReclamado")]
        public string amountReclaimed { get; set; }

        [DataMember(Name = "xTelefono")]
        public string phone { get; set; }

        [DataMember(Name = "xUsuario")]
        public string user { get; set; }

        [DataMember(Name = "xIdDocOrigen")]
        public string idDocOrigin { get; set; }

        [DataMember(Name = "xDescripExtend")]
        public string descripExtend { get; set; }

        [DataMember(Name = "xIdDocOAC")]
        public string idDocOAC { get; set; }
    }
}
