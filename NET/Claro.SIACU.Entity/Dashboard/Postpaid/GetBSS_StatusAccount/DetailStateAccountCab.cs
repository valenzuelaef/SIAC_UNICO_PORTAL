using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount
{
    [DataContract(Name = "xDetalleEstadoCuentaCab")]
    public class DetailStateAccountCab
    {
        public DetailStateAccountCab()
        {
            detailTrx = new List<DetailStateAccount>();
        }

        [DataMember(Name = "xNombreCliente")]
        public string clientName { get; set; }

        [DataMember(Name = "xDeudaActual")]
        public string currentDebt { get; set; }

        [DataMember(Name = "xDeudaVencida")]
        public string expiredDebt { get; set; }

        [DataMember(Name = "xTotalMontoDisputa")]
        public string totalAmountDispute { get; set; }

        [DataMember(Name = "xFechaUltFactura")]
        public string lastInvoiceDate { get; set; }

        [DataMember(Name = "xFechaUtlPago")]
        public string lastPaymentDate { get; set; }

        [DataMember(Name = "xCodCuenta")]
        public string codAccount { get; set; }

        [DataMember(Name = "xCodCuentaAlterna")]
        public string codAlternateAccount { get; set; }

        [DataMember(Name = "xDescUbigeo")]
        public string ubigeoDescription { get; set; }

        [DataMember(Name = "xTipoCliente")]
        public string clientType { get; set; }

        [DataMember(Name = "xEstadoCuenta")]
        public string stateAccount { get; set; }

        [DataMember(Name = "xFechaActivacion")]
        public string activationDate { get; set; }

        [DataMember(Name = "xCicloFacturacion")]
        public string billingCycle { get; set; }

        [DataMember(Name = "xLimiteCredito")]
        public string creditLimit { get; set; }

        [DataMember(Name = "xCreditScore")]
        public string creditScore { get; set; }

        [DataMember(Name = "xTipoPago")]
        public string typePay { get; set; }

        [DataMember(Name = "xDetalleTrx")]
        public List<DetailStateAccount> detailTrx { get; set; }
    }
}
