using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail
{
    [DataContract(Name = "AdditionalLocalTrafficDetailResponsePostPaid")]
    public class AdditionalLocalTrafficDetailResponse
    {
        [DataMember]
        public List<LocalTrafficDetail> ListTimMaxLocalTrafficDetail { get; set; }
        [DataMember]
        public List<LocalTrafficDetail> ListTimProLocalTrafficDetail { get; set; }
    }
}
