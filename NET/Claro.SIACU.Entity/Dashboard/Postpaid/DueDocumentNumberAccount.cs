using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "DueDocumentNumberAccountPostPaid")]
    public class DueDocumentNumberAccount
    {
        [DataMember]
        public string TipoServicio { get; set; }

        [DataMember]
        public string CodCuenta { get; set; }

        [DataMember]
        public string EstadoCuenta { get; set; }

        [DataMember]
        public decimal? PromedioFact { get; set; }

        [DataMember]
        public decimal? DeudaCorriente { get; set; }

        [DataMember]
        public decimal? DeudaVencida { get; set; }

        [DataMember]
        public decimal? DeudaCastigada { get; set; }

        [DataMember]
        public decimal? CantServicios { get; set; }

        [DataMember]
        public string IndCentralRiesgo { get; set; }

        [DataMember]
        public string NombreCliente { get; set; }

        [DataMember]
        public decimal? DeudaMovil { get; set; }

        [DataMember]
        public decimal? DeudaFija { get; set; }

        [DataMember]
        public decimal? DeudaVencidaMovil { get; set; }

        [DataMember]
        public decimal? DeudaVencidaFija { get; set; }

        [DataMember]
        public decimal? DeudaCastigadaMovil { get; set; }

        [DataMember]
        public decimal? DeudaCastigadaFija { get; set; }

        [DataMember]
        public string DNIAsociado { get; set; }

        [DataMember]
        public decimal? AntiguedadDeuda { get; set; }

        [DataMember]
        public decimal? TotalServicios { get; set; }
    }
}
