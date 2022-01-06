using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetInstants
{
    [DataContract(Name = "InstantsResponseDashboard")]
    public class InstantsResponse
    {
        [DataMember]
        public List<Instant> ListInstant { get; set; }
    }
}
