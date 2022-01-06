using Claro.SIACU.Entity.Dashboard;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetListInstant
{

    [DataContract(Name = "ListInstantResponseManagement")]
    public class ListInstantResponse
    {
        [DataMember]
        public List<Instant> listInstant { get; set; }
    }
}
