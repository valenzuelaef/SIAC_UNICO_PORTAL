using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetValidateLine
{
    [DataContract]
    public class responseData
    {
        [DataMember]
        public string codigoEtiqueta1 { get; set; }
        [DataMember]
        public string nombreEtiqueta1 { get; set; }
        [DataMember]
        public string codigoEtiqueta2 { get; set; }
        [DataMember]
        public string nombreEtiqueta2 { get; set; }

        [DataMember]
        public List<ListaOpcional> listaOpcional { get; set; }
    }
}
