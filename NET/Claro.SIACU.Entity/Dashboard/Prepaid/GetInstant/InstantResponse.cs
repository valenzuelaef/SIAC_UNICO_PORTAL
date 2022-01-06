using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetInstant
{
    [DataContract(Name = "InstantResponsePrepaid")]
    public class InstantResponse
    {
        [DataMember]
        public List<Instant> listInstant { get; set; }
    }
}
