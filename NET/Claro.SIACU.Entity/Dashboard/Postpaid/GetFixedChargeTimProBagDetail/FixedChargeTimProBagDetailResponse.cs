using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail
{
    [DataContract(Name = "FixedChargeTimProBagDetailResponsePostPaid")]
        public class FixedChargeTimProBagDetailResponse 
        {
        [DataMember]
        public List<FixedChargeTimProBagDetail> ListFixedChargeTimProBagDetail { get; set; } 
        }
}
