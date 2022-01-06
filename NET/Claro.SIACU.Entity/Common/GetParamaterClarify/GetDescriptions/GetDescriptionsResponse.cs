using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetParamaterClarify.GetDescriptions
{
    [DataContract]
    public class GetDescriptionsResponse
    {
        [DataMember]
        public AuditResponse auditResponse { get; set; }

        [DataMember]
        public List<DatosParametroSiacType> datosParametroClarify { get; set; }
        
    }
}
