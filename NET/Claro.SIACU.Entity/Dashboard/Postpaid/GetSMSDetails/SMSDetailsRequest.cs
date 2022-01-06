using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSMSDetails
{
    [DataContract(Name = "SMSDetailsRequestPostPaid")]
    public class SMSDetailsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public string Msisdn { get; set; } 
    }
}
