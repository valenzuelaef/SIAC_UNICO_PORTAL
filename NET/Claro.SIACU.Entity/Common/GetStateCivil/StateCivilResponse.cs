using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetStateCivil
{
    [DataContract(Name = "StateCivilResponseCommon")]
    public class StateCivilResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
