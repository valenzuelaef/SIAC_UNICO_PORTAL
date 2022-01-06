using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetBagList
{
    [DataContract(Name = "BagListResponseCommon")]
    public class BagListResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
