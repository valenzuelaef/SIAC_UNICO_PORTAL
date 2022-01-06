using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Common.GetConsultarTecVoLTE
{
    [DataContract(Name = "ConsultarTecVoLTERequestCommon")]
    public class ConsultarTecVoLTERequest : Claro.Entity.Request 
    {
        [DataMember]
        public string serieVOLTE { get; set; }
        [DataMember]
        public List<Entity.ItemOpcional> lstRequestOpcional { get; set; }
    }
}
