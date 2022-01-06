using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetGroupBagList
{
    [DataContract(Name = "GroupBagListResponseCommon")]
    public class GroupBagListResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
