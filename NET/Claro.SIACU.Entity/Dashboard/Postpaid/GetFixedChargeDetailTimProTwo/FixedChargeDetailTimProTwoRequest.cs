using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo
{
    [DataContract(Name = "FixedChargeDetailTimProTwoRequestPostPaid")]
    public class FixedChargeDetailTimProTwoRequest: Claro.Entity.Request 
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
