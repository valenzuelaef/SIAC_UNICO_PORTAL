using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount
{
    [DataContract(Name = "consultarEstadoCuenta")]
    public class ConsultStateAccount
    {
        [DataMember(Name = "txId")]
        public string txId { get; set; }

        [DataMember(Name = "pCodAplicacion")]
        public string codAplication { get; set; }

        [DataMember(Name = "pUsuarioAplic")]
        public string userAplic { get; set; }

        [DataMember(Name = "pTipoConsulta")]
        public string queryType { get; set; }

        [DataMember(Name = "pTipoServicio")]
        public string serviceType { get; set; }

        [DataMember(Name = "pCliNroCuenta")]
        public string numAccount { get; set; }

        [DataMember(Name = "pNroTelefono")]
        public string phoneNumber { get; set; }

        [DataMember(Name = "pFlagSoloSaldo")]
        public string flagOnlyBalance { get; set; }

        [DataMember(Name = "pFlagSoloDisputa")]
        public string flagOnlyDispute { get; set; }

        [DataMember(Name = "pFechaDesde")]
        public string dateFrom { get; set; }

        [DataMember(Name = "pFechaHasta")]
        public string dateTo { get; set; }

        [DataMember(Name = "pTamanoPagina")]
        public string sizePage { get; set; }

        [DataMember(Name = "pNroPagina")]
        public string numPage { get; set; }
    }
}
