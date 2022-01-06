using Claro.SIACU.Entity.Dashboard;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetInstant
{
    [DataContract(Name = "InstantRequestManagement")]
    public class InstantRequest: Claro.Entity.Request 
    {
        [DataMember]
        public Instant instant { get; set; }

        [DataMember]
        public List<Instant> lstInstant { get; set; }

    }
}
