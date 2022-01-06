using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetTelephoneType
{
    [DataContract(Name = "TelephoneTypeResponseCommon")]
    public class TelephoneTypeResponse
    {
        [DataMember]
        public List<ListItem> TelephoneTypes { get; set; }
    }
}
