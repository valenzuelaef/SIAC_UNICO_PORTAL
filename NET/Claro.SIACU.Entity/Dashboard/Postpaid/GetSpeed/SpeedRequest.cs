using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSpeed
{
    [DataContract(Name = "SpeedRequestPostPaid")]
    public class SpeedRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strMsisdn { get; set; }
        [DataMember]
        public Claro.Entity.AuditRequest objAudit { get; set; }
    }
}
