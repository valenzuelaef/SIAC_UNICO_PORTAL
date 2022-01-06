using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetMonitoringCases
{
    [DataContract(Name = "MonitoringCasesRequestPostPaid")]
    public class MonitoringCasesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CaseId { get; set; }
        [DataMember]
        public string CustomerAccount { get; set; }
        [DataMember]
        public string DateFrom { get; set; }
        [DataMember]
        public string DateTo { get; set; }
    }
}
