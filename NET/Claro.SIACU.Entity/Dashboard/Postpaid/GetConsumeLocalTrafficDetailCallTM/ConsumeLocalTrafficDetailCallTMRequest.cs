using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM
{
    [DataContract(Name = "ConsumeLocalTrafficDetailCallTMRequestPostPaid")]
    public class ConsumeLocalTrafficDetailCallTMRequest : Claro.Entity.Request
    {
        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public string Msisdn { get; set; }

        [DataMember]
        public string TypeCallTM { get; set; }

       
    }
}
