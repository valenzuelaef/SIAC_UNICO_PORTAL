using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTrackingDetail
{
    [DataContract(Name = "TrackingDetailRequestPostPaid")]
    public class TrackingDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public double IdCaso { get; set; }
    }
}
