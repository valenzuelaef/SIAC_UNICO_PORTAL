using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetSearchType
{
    [DataContract(Name = "SearchTypeResponseCommon")]
    public class SearchTypeResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
