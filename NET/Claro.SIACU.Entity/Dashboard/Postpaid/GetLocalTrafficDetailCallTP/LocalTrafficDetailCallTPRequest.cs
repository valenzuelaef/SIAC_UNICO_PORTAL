using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP
{
    [DataContract(Name = "LocalTrafficDetailCallTPRequestPostPaid")]
    public  class LocalTrafficDetailCallTPRequest : Claro.Entity.Request
    {
        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public string Msisdn { get; set; }

        [DataMember]
        public string TypeCallTP { get; set; }
    }
}
