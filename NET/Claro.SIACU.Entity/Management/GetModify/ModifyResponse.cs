using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetModify
{
    [DataContract(Name = "ModifyResponseManagement")]
    public class ModifyResponse
    {
        [DataMember]
        public List<Banner> ListBanner { get; set; }
    }
}
