using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSubTableTracking
{
    [DataContract(Name = "SubTableTrackingResponsePostPaid")]
    public class SubTableTrackingResponse
    {
        [DataMember]
        public List<QueryOAC> QueryOACs { get; set; }
    }
}
