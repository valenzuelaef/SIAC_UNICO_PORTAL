using Claro.SIACU.Entity.Dashboard;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetInstantById
{
    [DataContract(Name = "InstantByIdResponseManagement")]
    public class InstantByIdResponse
    {
        [DataMember]
        public Instant instant { get; set; }
    }
}
