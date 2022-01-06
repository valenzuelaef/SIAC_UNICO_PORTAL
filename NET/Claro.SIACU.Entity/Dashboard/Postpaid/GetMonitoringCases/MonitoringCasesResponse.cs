using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetMonitoringCases
{
    [DataContract(Name = "MonitoringCasesResponsePostPaid")]
    public class MonitoringCasesResponse
    {
        [DataMember]
        public List<MonitoringCases> ListMonitoringCases { get; set; }
    }
}
