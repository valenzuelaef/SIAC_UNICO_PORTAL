using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Response
{
    [DataContract]
    public class ConsultarContratosLineaResponse
    {
        [DataMember]
        public ResponseAudit responseAudit { get; set; }
        [DataMember]
        public ResponseDataContrato responseDataContrato { get; set; }

    }
}
