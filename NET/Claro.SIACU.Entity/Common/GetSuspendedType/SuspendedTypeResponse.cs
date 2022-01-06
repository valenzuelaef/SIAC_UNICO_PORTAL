using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetSuspendedType
{
    [DataContract(Name = "SuspendedTypeResponseCommon")]
    public class SuspendedTypeResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
