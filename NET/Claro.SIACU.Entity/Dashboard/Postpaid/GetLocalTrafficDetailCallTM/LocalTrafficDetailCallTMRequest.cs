using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM
{
    [DataContract(Name = "LocalTrafficDetailCallTMRequestPostPaid")]
    public  class LocalTrafficDetailCallTMRequest : Claro.Entity.Request
    {
        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public string Msisdn { get; set; }

        [DataMember]
        public string TypeCallTM { get; set; }

       
    }
}
