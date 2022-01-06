using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetThirdTableTracking
{
    [DataContract(Name = "ThirdTableTrackingResponsePostPaid")]
    public class ThirdTableTrackingResponse
    {
        [DataMember]
        public List<QueryOAC> QueryOACs { get; set; }
    }
}
