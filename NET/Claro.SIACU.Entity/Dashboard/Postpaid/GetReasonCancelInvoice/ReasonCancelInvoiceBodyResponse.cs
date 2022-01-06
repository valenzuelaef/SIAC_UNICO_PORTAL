using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetReasonCancelInvoice
{
    public class ReasonCancelInvoiceBodyResponse
    {
        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }

        [DataMember(Name = "mensajeRespuesta")]
        public string mensajeRespuesta { get; set; }

        [DataMember(Name = "listaMotivos")]
        public List<listaMotivos> listaMotivos { get; set; }
    }

    public class listaMotivos
    {
        [DataMember(Name = "codigo")]
        public string codigo { get; set; }

        [DataMember(Name = "descripcion")]
        public string descripcion { get; set; }
    }
}
