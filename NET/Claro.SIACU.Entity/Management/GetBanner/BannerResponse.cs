using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetBanner
{
    [DataContract(Name = "BannerResponseManagement")]
    public class BannerResponse
    {
        [DataMember]
        public List<Banner> ListBanner { get; set; }
    }
}
