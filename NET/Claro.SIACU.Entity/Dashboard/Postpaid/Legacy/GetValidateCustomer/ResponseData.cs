using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer
{
    [DataContract]
    public class ResponseData
    {
        [DataMember]
        public string codigoEtiqueta { get; set; }

        [DataMember]
        public string nombreEtiqueta { get; set; }

        [DataMember]
        public string bonoElegido { get; set; }

        [DataMember]
        public string bonoLinea { get; set; }

        [DataMember]
        public string estado { get; set; }

        public List<ListaOpcional> listaOpcional { get; set; }

    }
}
