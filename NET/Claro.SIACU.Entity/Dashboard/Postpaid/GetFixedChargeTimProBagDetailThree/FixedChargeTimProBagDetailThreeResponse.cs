using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree
{
    [DataContract(Name = "FixedChargeTimProBagDetailThreeResponsePostPaid")]
        public class FixedChargeTimProBagDetailThreeResponse 
        {
        [DataMember]
        public List<FixedChargeTimProBagDetailThree> ListFixedChargeTimProBagDetailThree { get; set; } 
        }
}
