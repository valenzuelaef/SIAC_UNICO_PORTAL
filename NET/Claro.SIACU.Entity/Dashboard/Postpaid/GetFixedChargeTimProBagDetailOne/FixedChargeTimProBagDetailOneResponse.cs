using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne
{
    [DataContract(Name = "FixedChargeTimProBagDetailOneResponsePostPaid")]
        public class FixedChargeTimProBagDetailOneResponse 
        {
        [DataMember]
        public List<FixedChargeTimProBagDetailOne> ListFixedChargeTimProBagDetailOne { get; set; } 
        }
}
