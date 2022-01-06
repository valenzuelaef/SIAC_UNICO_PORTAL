using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetConsultaUDB
{
    [DataContract(Name = "ConsultaUDBResponseCommon")]
    public class ConsultaUDBResponse
    {
         [DataMember]
         public Entity.AuditResponse oAuditResponse { get; set; }
         [DataMember]
         public AccionUDB oAccionResponse { get; set; }
    }
}
