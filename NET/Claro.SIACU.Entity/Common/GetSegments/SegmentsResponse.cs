using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetSegments
{
    [DataContract(Name = "SegmentsResponseCommon")]
    public class SegmentsResponse
    {
        [DataMember]
        public List<Entity.Dashboard.Segment> ListSegment { get; set; }
        [DataMember]
        public string code { get; set; }
    }
}
