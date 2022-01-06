using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo
{
    [DataContract(Name = "FixedChargeDetailTimProBagTwoResponsePostPaid")]
        public class FixedChargeDetailTimProBagTwoResponse 
        {
        [DataMember]
        public List<FixedChargeDetailTimProBagTwo> ListFixedChargeDetailTimProBagTwo { get; set; }
        }
}
