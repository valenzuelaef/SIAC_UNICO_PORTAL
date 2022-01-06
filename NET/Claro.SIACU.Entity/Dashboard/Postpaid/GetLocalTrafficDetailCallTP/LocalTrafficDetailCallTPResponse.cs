using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP
{
    [DataContract(Name = "LocalTrafficDetailCallTPResponsePostPaid")]
    public class LocalTrafficDetailCallTPResponse
    {
        [DataMember]
        public List<LocalTrafficDetailCallTP> ListLocalTrafficDetailCallTP { get; set; }

        [DataMember]
        public string LocalTrafficQuantityCallTP { get; set; } 
    }
}
