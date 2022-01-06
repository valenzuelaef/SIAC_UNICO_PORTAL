using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge
{
    [DataContract(Name = "ConsumptionHistoricalRechargeRequestPospaid")]
   public  class ConsumptionHistoricalRechargeRequestPospaid: Claro.Entity.Request
    {
        [DataMember]
        public string strTypeConsumption { get; set; }
        [DataMember]
        public string strDateStart { get; set; }
        [DataMember]
        public string strMsisdn { get; set; }
        [DataMember]
        public string strDateEnd { get; set; }
        [DataMember]
        public Claro.Entity.AuditRequest objAudit { get; set; }
    }
}
 