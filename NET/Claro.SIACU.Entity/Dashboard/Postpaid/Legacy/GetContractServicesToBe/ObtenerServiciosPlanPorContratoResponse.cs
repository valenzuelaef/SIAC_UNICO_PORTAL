using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetContractServicesToBe
{
    [DataContract]
    public class ObtenerServiciosPlanPorContratoResponse
    {
        [DataMember]
        public ResponseAudit responseAudit { get; set; }
        [DataMember]
        public ResponseData responseData { get; set; }
    }
}
