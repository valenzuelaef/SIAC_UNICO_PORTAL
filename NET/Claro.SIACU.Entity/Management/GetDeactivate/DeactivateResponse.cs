using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetDeactivate
{
    [DataContract(Name = "DeactivateResponseManagement")]
    public class DeactivateResponse
    {
        [DataMember]
        public List<Banner> ListBanner { get; set; }
    }
}
