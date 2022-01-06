using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetPetitionsType
{
    [DataContract(Name = "PetitionsResponseCommon")]
    public class PetitionsTypeResponse
    {
        [DataMember]
        public List<ListItem> PetitionsTypes { get; set; }
    }
}
