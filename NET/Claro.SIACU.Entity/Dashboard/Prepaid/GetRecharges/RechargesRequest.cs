using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetRecharges
{
    [DataContract(Name = "RechargesRequestPrePaid")]
    public class RechargesRequest : Claro.Entity.Request
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
        public string strtypeQuery { get; set; }
        [DataMember]
        public string strMovementType { get; set; }
        [DataMember]
        public string strcreditoDebito { get; set; }
        [DataMember]
        public string PlataformaIT { get; set; }
    }
}
