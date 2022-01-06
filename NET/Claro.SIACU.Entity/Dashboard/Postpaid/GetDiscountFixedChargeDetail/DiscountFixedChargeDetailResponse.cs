using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail
{
    [DataContract(Name = "DiscountFixedChargeDetailResponsePostPaid")]
    public class DiscountFixedChargeDetailResponse 
        {
        [DataMember]
        public List<DiscountFixedChargeDetail> ListDiscountFixedChargeDetail { get; set; } 
        }
}
