using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Coliving.getConsultaLineaCuenta
{
    [DataContract]
    public class ConsultaLineaResponse
    {
        [DataMember]
        public string ResponseValue { get; set; }
        [DataMember]
        public string ResponseDescription { get; set; }
        [DataMember]
        public Itm itm { get; set; }
    }
}
