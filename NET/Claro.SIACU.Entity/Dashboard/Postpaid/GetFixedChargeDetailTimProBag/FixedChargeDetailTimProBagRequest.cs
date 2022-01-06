using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag
{
    [DataContract(Name = "FixedChargeDetailTimProBagRequestPostPaid")]
    public class FixedChargeDetailTimProBagRequest :Claro.Entity.Request
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
