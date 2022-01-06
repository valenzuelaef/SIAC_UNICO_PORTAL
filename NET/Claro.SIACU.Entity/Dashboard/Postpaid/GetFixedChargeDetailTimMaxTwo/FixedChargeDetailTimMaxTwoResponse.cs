using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo
{
    [DataContract(Name = "FixedChargeDetailTimMaxTwoResponsePostPaid")]
    public class FixedChargeDetailTimMaxTwoResponse 
        {
            [DataMember]
        public List<FixedChargeDetailTimMaxTwo> ListFixedChargeDetailTimMaxTwo { get; set; } 
        }
}
