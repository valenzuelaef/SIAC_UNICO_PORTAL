using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail
{
    [DataContract(Name = "FixedChargeTimProBagDetailRequestPostPaid")]
    public class FixedChargeTimProBagDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
