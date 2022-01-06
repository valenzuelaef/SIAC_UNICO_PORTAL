using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "QueryOACPostPaid")]
    public class QueryOAC
    {
        [DataMember]
        public string IdClarify { get; set; }
        [DataMember]
        public string CodigoCliente { get; set; }
        [DataMember]
        public string ContactoCliente { get; set; }
        [DataMember]
        public string Recurrencia { get; set; }
        [DataMember]
        public string NombreCliente { get; set; }
        [DataMember]
        public string UbicacionCaso { get; set; }
        [DataMember]
        public string NivelCaso { get; set; }
        [DataMember]
        public string EstadoCaso { get; set; }
        [DataMember]
        public string FechaCaso { get; set; }
        [DataMember]
        public string UsuarioRegistroCaso { get; set; }
        [DataMember]
        public string PropietarioCaso { get; set; }
        [DataMember]
        public string ComentarioCaso { get; set; }
        [DataMember]
        public string FechaCierreCaso { get; set; }
        [DataMember]
        public string ReaperturaCaso { get; set; }
        [DataMember]
        public string UsuarioNombre { get; set; }
        [DataMember]
        public string NumeroDocumentoCaso { get; set; }
        [DataMember]
        public string IdReclamoCaso { get; set; }
        [DataMember]
        public string TipoDocumentoCaso { get; set; }
        [DataMember]
        public string VariableReclamoCaso { get; set; }
        [DataMember]
        public string TipoTransaccionCaso { get; set; }
        [DataMember]
        public string MontoCaso { get; set; }
        [DataMember]
        public string ReclamoOhxactCaso { get; set; }
        [DataMember]
        public string CasoIdClarify { get; set; }
        [DataMember]
        public string MemdvNombre { get; set; }
        [DataMember]
        public string EnvimdEnvio { get; set; }
        [DataMember]
        public string EnvimdMonto { get; set; }
    }
}
