using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Request
{
    [DataContract]
    public class ConsultarContratosLineaRequest
    {
        public ConsultarContratosLineaRequest()
        {
            listaOpcional = null;

        }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public string flagMigrado { get; set; }
        [DataMember]
        public List<ListaOpcional> listaOpcional { get; set; }

    }
}
