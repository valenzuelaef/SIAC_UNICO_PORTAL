using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail
{
    [DataContract(Name = "ConsumeLocalTrafficDetailResponsePostPaid")]
    public class ConsumeLocalTrafficDetailResponse
    {
        [DataMember]
        public List<LocalTrafficDetail> ListTimMaxLocalTrafficDetail { get; set; }
        [DataMember]
        public List<LocalTrafficDetail> ListTimProLocalTrafficDetail { get; set; }
    }
}
