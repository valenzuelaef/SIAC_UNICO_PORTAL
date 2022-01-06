using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetCalls
{
    [DataContract(Name = "CallsRequestPrePaid")]
    public class CallsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Msisdn { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
        [DataMember]
        public string TrafficType { get; set; }
        [DataMember]
        public string TypeQuery { get; set; }
        [DataMember]
        public bool FlagVisualize { get; set; }
        [DataMember]
        public string TelephoneTipi { get; set; }
        [DataMember]
        public bool isTFI { get; set; }

    }
}
