using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetSex
{
    [DataContract(Name = "SexResponseCommon")]
    public class SexResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
