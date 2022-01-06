using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne
{
    [DataContract(Name = "FixedChargeDetailTimProOneResponsePostPaid")]
        public class FixedChargeDetailTimProOneResponse 
        {
            [DataMember]
            public List<FixedChargeDetailTimProOne> ListFixedChargeDetailTimProOne { get; set; } 
        }
}
