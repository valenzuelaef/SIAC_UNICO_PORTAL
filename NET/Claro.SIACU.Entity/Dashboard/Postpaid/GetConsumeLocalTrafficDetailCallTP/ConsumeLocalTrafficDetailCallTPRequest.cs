using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP
{
    [DataContract(Name = "ConsumeLocalTrafficDetailCallTPRequestPostPaid")]
    public class ConsumeLocalTrafficDetailCallTPRequest : Claro.Entity.Request
    {
        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public string Msisdn { get; set; }

        [DataMember]
        public string TypeCallTP { get; set; }
    }
}
