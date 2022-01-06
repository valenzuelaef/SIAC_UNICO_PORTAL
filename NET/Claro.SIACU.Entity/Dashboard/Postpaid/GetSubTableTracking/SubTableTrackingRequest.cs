using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSubTableTracking
{
    [DataContract(Name = "SubTableTrackingRequestPostPaid")]
    public class SubTableTrackingRequest : Claro.Entity.Request
    {
        [DataMember]
        public double IdCaso { get; set; }
    }
}
