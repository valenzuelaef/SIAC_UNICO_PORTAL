using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM
{
    [DataContract(Name = "ConsumeLocalTrafficDetailCallTMResponsePostPaid")]
    public class ConsumeLocalTrafficDetailCallTMResponse
    {
        [DataMember]
        public List<ConsumeLocalTrafficDetailCallTM> ListConsumeLocalTrafficDetailCallTM { get; set; } 

        [DataMember]
        public string ConsumeLocalTrafficQuantityCallTM { get; set; } 
    }
}
