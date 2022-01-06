using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo
{
    [DataContract(Name = "FixedChargeTimProBagDetailTwoResponsePostPaid")]
        public class FixedChargeTimProBagDetailTwoResponse 
        { 
        [DataMember]
        public List<FixedChargeTimProBagDetailTwo> ListFixedChargeTimProBagDetailTwo { get; set; } 
        }
}
