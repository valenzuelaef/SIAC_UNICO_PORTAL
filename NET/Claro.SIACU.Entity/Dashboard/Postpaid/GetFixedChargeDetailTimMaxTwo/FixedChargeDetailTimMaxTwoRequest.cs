using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo
{
    [DataContract(Name = "FixedChargeDetailTimMaxTwoRequestPostPaid")]
    public class FixedChargeDetailTimMaxTwoRequest: Claro.Entity.Request 
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
