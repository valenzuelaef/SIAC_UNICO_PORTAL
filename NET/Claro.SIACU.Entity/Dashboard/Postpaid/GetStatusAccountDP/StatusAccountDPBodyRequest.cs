using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccountDP
{
    public class StatusAccountDPBodyRequest
    {
        [DataMember(Name = "txId")]
        public string txId { get; set; }

        [DataMember(Name = "codAplicacion")]
        public string codAplicacion { get; set; }

        [DataMember(Name = "usuarioAplic")]
        public string usuarioAplic { get; set; }

        [DataMember(Name = "tipoConsulta")]
        public string tipoConsulta { get; set; }

        [DataMember(Name = "tipoServicio")]
        public string tipoServicio { get; set; }

        [DataMember(Name = "cliNroCuenta")]
        public string cliNroCuenta { get; set; }

        [DataMember(Name = "nroTelefono")]
        public string nroTelefono { get; set; }

        [DataMember(Name = "flagSoloSaldo")]
        public string flagSoloSaldo { get; set; }

        [DataMember(Name = "flagSoloDisputa")]
        public string flagSoloDisputa { get; set; }

        [DataMember(Name = "fechaDesde")]
        public string fechaDesde { get; set; }

        [DataMember(Name = "fechaHasta")]
        public string fechaHasta { get; set; }

        [DataMember(Name = "tamanoPagina")]
        public decimal tamanoPagina { get; set; }

        [DataMember(Name = "nroPagina")]
        public decimal nroPagina { get; set; }
    }
}