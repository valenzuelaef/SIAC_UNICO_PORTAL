using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Common.GetConsultaUDB
{
    [DataContract(Name = "ConsultaUDBRequestCommon")]
    public class ConsultaUDBRequest : Claro.Entity.Request 
    {
        [DataMember]
        public AccionUDB oAccionRequest { get; set; }
    }
}
