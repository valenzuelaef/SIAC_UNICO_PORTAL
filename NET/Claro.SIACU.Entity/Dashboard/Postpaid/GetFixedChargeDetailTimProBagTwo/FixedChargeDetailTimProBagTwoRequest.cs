using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo
{
    [DataContract(Name = "FixedChargeDetailTimProBagTwoRequestPostPaid")]
    public class FixedChargeDetailTimProBagTwoRequest : Claro.Entity.Request
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
