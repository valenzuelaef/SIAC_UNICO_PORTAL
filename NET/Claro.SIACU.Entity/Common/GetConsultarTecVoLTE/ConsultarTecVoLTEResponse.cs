using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetConsultarTecVoLTE
{
    [DataContract(Name = "ConsultarTecVoLTEResponseCommon")]
    public class ConsultarTecVoLTEResponse
    {
        [DataMember]
        public Entity.AuditResponse oAuditResponse { get; set; }
        [DataMember]
        public string codigoMaterial { get; set; }
        [DataMember]
        public string existeChip { get; set; }
        [DataMember]
        public string autenticaVOLTE { get; set; }
        [DataMember]
        public string codigoResultado { get; set; }
        [DataMember]
        public string mensajeResultado { get; set; }
        [DataMember]
        public List<Entity.ItemOpcional> lstResponseOpcional { get; set; }
    }
}
