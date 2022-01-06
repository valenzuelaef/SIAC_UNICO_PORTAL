using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetCallDetail
{
    [DataContract(Name = "CallDetailResponsePrePaid")]
    public class CallDetailResponse
    {
        [DataMember]
        public List<Call> listCall { get; set; }
    }
}
