using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax
{
    [DataContract(Name = "FixedChargeDetailTimMaxResponsePostPaid")]
    public class FixedChargeDetailTimMaxResponse 
        {
            [DataMember]
        public List<FixedChargeDetailTimMax> ListFixedChargeDetailTimMax { get; set; } 
           
        }
}
