using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ReclosuresPostPaid")]
    public class Reclosures
    {
        [DataMember]
        public string CasoCierre { get; set; }
        [DataMember]
        public string Resolucion { get; set; }
        [DataMember]
        public string Resultado { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string FechaEnvioBanco { get; set; }
        [DataMember]
        public string CasoReapertura { get; set; }
        [DataMember]
        public string ReapvFaseClarify { get; set; }
        [DataMember]
        public string ReapdProcesoReapertura { get; set; }
    }
}
