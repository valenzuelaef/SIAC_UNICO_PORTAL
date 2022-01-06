using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro
{
    [DataContract(Name = "FixedChargeDetailTimProResponsePostPaid")]
        public class FixedChargeDetailTimProResponse 
        {
            [DataMember]
            public List<FixedChargeDetailTimPro> ListFixedChargeDetailTimPro { get; set; } 
        }
}
