using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo
{
    [DataContract(Name = "FixedChargeDetailTimProTwoResponsePostPaid")]
        public class FixedChargeDetailTimProTwoResponse 
        {
            [DataMember]
            public List<FixedChargeDetailTimProTwo> ListFixedChargeDetailTimProTwo { get; set; } 
        }
}
