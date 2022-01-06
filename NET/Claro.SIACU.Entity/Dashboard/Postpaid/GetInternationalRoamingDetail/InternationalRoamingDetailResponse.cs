using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetInternationalRoamingDetail
{
    [DataContract(Name = "InternationalRoamingDetailResponsePostPaid")]
    public class InternationalRoamingDetailResponse
    {
        [DataMember]
        public List<CallDetail> ListCallDetail { get; set; }
    }
}
