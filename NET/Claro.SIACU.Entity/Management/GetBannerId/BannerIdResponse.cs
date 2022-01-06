using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetBannerId
{
    [DataContract(Name = "BannerIdResponseManagement")]
    public class BannerIdResponse
    {
        [DataMember]
        public Banner Banner { get; set; }
    }
}
