using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP
{
    [DataContract(Name = "ConsumeLocalTrafficDetailCallTPResponsePostPaid")]
    public class ConsumeLocalTrafficDetailCallTPResponse
    {
        [DataMember]
        public List<ConsumeLocalTrafficDetailCallTP> ListConsumeLocalTrafficDetailCallTP { get; set; } 

        [DataMember]
        public string ConsumeLocalTrafficQuantityCallTP { get; set; } 
        
    }
}
