using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetThirdTableTracking
{
    [DataContract(Name = "ThirdTableTrackingRequestPostPaid")]
    public class ThirdTableTrackingRequest : Claro.Entity.Request
    {
        [DataMember]
        public double CaseClaimId { get; set; }
    }
}
