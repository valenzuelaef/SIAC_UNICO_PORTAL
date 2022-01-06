using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetConsultaSeguridad
{
    [DataContract(Name = "ConsulSeguridadResponseCommon")]
    public class ConsulSeguridadResponse
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
