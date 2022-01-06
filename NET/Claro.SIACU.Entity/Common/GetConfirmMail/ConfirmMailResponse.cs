using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetConfirmMail
{
    [DataContract(Name = "ConfirmMailResponseCommon")]
    public class ConfirmMailResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
