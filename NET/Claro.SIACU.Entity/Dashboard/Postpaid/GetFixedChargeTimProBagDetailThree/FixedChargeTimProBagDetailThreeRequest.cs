using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree
{
    [DataContract(Name = "FixedChargeTimProBagDetailThreeRequestPostPaid")]
    public class FixedChargeTimProBagDetailThreeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
