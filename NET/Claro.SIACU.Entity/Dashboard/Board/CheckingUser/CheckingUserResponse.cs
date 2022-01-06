using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Board.CheckingUser
{
    [DataContract(Name = "CheckingUserResponse")]
    public class CheckingUserResponse
    {
        [DataMember]
        public List<Entity.Dashboard.ConsulSeguridad> consultasSeguridad { get; set; }

        [DataMember]
        public string IdTransaccion { get; set; }

        [DataMember]
        public string CodeErr { get; set; }

        [DataMember]
        public string MsgErr { get; set; }
    }
}
