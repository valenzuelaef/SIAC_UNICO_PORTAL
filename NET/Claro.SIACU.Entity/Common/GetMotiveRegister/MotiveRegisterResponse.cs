using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetMotiveRegister
{
    [DataContract(Name = "MotiveRegisterResponseCommon")]
    public class MotiveRegisterResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
