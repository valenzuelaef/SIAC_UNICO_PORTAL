using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetBannerId
{
    [DataContract(Name = "BannerIdRequestManagement")]
    public class BannerIdRequest : Claro.Entity.Request
    {
        [DataMember]
        public int ID_BANNER { get; set; }
    }
}
