using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM
{
    [DataContract(Name = "LocalTrafficDetailCallTMResponsePostPaid")]
    public class LocalTrafficDetailCallTMResponse
    {
        [DataMember]
        public List<LocalTrafficDetailCallTM> ListLocalTrafficDetailCallTM { get; set; }

        [DataMember]
        public string LocalTrafficQuantityCallTM { get; set; } 
    }
}
