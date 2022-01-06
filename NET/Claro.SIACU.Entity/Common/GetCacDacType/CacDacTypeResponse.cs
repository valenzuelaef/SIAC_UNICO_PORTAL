using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetCacDacType
{
    [DataContract(Name = "CacDacTypeResponseCommon")]
    public class CacDacTypeResponse
    {
        [DataMember]
        public List<ListItem> CacDacTypes { get; set; }
    }
}
