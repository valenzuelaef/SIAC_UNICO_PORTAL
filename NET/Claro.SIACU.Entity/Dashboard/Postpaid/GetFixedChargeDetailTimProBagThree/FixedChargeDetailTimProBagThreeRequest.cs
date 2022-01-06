using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree
{
    [DataContract(Name = "FixedChargeDetailTimProBagThreeRequestPostPaid")]
    public class FixedChargeDetailTimProBagThreeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
