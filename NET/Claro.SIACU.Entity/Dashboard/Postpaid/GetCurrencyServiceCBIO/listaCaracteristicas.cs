using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class listaCaracteristicas
    {
        [DataMember]
        public string descripcionGrupo { get; set; }
        [DataMember]
        public string numeroGrupo { get; set; }
        [DataMember]
        public string poServicio { get; set; }
        [DataMember]
        public string cargoFijo { get; set; }
        [DataMember]
        public string spcodedes { get; set; }
        [DataMember]
        public string tipoPO { get; set; }
        [DataMember]
        public string categoriaServicio { get; set; }
        [DataMember]
        public string validoDesde { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string tipoServicio { get; set; }
        [DataMember]
        public string monto { get; set; }
    }
}
