using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTrackingDetail
{
    [DataContract(Name = "TrackingDetailResponsePostPaid")]
    public class TrackingDetailResponse
    {
        [DataMember]
        public List<QueryOAC> QueryOACs { get; set; }
    }
}
