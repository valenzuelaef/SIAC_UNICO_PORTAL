using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag
{
    [DataContract(Name = "FixedChargeDetailTimProBagResponsePostPaid")]
        public class FixedChargeDetailTimProBagResponse 
        {
            [DataMember]
            public List<FixedChargeDetailTimProBag> ListFixedChargeDetailTimProBag { get; set; } 
        }
}
