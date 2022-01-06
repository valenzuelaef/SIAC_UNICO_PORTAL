using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree
{
    [DataContract(Name = "FixedChargeDetailTimProBagThreeResponsePostPaid")]
        public class FixedChargeDetailTimProBagThreeResponse 
        {
        [DataMember]
        public List<FixedChargeDetailTimProBagThree> ListFixedChargeDetailTimProBagThree { get; set; } 

        }
}
