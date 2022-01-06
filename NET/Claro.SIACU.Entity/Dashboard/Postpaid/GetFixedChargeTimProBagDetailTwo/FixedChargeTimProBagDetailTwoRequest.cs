using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo
{
    [DataContract(Name = "FixedChargeTimProBagDetailTwoRequestPostPaid")]
    public class FixedChargeTimProBagDetailTwoRequest: Claro.Entity.Request
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
