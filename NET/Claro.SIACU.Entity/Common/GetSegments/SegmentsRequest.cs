using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetSegments
{
    [DataContract(Name = "SegmentsRequestCommon")]
    public class SegmentsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string TypeDocument { get; set; }

        [DataMember]
        public string NumberDocument { get; set; }
    }
}
