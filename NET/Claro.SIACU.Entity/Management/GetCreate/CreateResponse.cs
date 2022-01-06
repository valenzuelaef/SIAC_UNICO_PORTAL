using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetCreate
{
    [DataContract(Name = "CreateResponseManagement")]
    public class CreateResponse
    {
        [DataMember]
        public List<Banner> ListBanner { get; set; }
    }
}
