using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Board.GetConsultaSeguridad
{
    public class ConsulSeguridadRequest : Claro.Entity.Request
    {
        
        [DataMember]
        public string IdTrans { get; set; }

        [DataMember]
        public string IpAplicacion { get; set; }

        [DataMember]
        public string Aplicacion { get; set; }

        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public long AppCod { get; set; }
    }
}
