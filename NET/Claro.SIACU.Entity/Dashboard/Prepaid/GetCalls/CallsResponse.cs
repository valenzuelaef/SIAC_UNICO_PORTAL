using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetCalls
{
    [DataContract(Name = "CallsResponsePrePaid")]
    public class CallsResponse
    {
        [DataMember]
        public List<Call> listCall { get; set; }
        [DataMember]
        public bool resultTipi { get; set; }
    }
}
