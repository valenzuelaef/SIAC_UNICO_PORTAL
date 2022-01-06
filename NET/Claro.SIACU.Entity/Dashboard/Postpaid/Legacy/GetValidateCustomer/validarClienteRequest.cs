using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer
{
    [DataContract]
    public class validarClienteRequest 
    {
        [DataMember]
        public string tipoDocumento { get; set; }

        [DataMember]
        public string numeroDocumento { get; set; }

        [DataMember]
        public List<ListaOpcional> listaOpcional { get; set; }

    }
}
