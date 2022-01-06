using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne
{
    [DataContract(Name = "FixedChargeDetailTimProBagOneResponsePostPaid")]
        public class FixedChargeDetailTimProBagOneResponse 
        {
            [DataMember]
            public List<FixedChargeDetailTimProBagOne> ListFixedChargeDetailTimProBagOne { get; set; } 
        }
}
