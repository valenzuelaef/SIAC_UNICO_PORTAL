using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail
{
    [DataContract(Name = "DiscountFixedChargeDetailRequestPostPaid")]
    public class DiscountFixedChargeDetailRequest: Claro.Entity.Request 
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
