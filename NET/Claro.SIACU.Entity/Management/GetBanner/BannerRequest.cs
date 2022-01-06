using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetBanner
{
    [DataContract(Name = "BannerRequestManagement")]
    public class BannerRequest : Claro.Entity.Request
    {
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
