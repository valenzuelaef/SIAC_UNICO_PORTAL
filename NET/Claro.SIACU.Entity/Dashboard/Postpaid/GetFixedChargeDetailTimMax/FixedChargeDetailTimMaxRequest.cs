using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax
{
    [DataContract(Name = "FixedChargeDetailTimMaxRequestPostPaid")]
    public class FixedChargeDetailTimMaxRequest: Claro.Entity.Request 
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
