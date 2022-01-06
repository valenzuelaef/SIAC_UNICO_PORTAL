using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetContractServicesToBe
{
    [DataContract]
    public class ObtenerServiciosPlanPorContratoRequest
    {
        [DataMember]
        public string contractIdPub { get; set; }
        [DataMember]
        public string flagConsulta { get; set; }
        [DataMember]
        public string validaExcluyente { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public List<ListaOpcional> listaOpcional { get; set; }

    }
}
