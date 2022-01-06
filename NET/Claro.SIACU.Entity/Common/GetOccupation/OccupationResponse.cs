using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetOccupation
{
    [DataContract(Name = "OccupationResponseCommon")]
    public class OccupationResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
