using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount
{
    [DataContract(Name = "BSS_StatusAccountRequest")]
    public class BSS_StatusAccountRequest : Claro.Entity.Request
    {

        [DataMember(Name = "tipoConsulta")]
        public string queryType { get; set; }

        [DataMember(Name = "cuenta")]
        public string account { get; set; }

        [DataMember(Name = "periodo")]
        public string period { get; set; }

        [DataMember(Name = "numeroDocumentos")]
        public string numberDocuments { get; set; }

        [DataMember(Name = "consultarEstadoCuenta")]
        public ConsultStateAccount consultStateAccount { get; set; }
    }
}
