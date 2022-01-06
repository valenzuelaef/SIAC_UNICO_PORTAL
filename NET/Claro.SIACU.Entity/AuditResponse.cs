using System.Runtime.Serialization;

namespace Claro.SIACU.Entity
{
    [DataContract(Name = "AuditResponse")]
    public class AuditResponse
    {
        [DataMember]
        public string idTransaccion { get; set; }
        [DataMember]
        public string codigoRespuesta { get; set; }
        [DataMember]
        public string mensajeRespuesta { get; set; }
    }
}
