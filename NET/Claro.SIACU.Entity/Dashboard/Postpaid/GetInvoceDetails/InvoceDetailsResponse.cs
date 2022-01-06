using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetInvoceDetails
{
    public class InvoceDetailsResponse
    {
        public responseAudit responseAudit { get; set; }
        public responseData responseData { get; set; }
    }
     public class responseAudit
    {
        public string idTransaccion { get; set; }
        public string codigoRespuesta { get; set; }
        public string mensajeRespuesta { get; set; }
    }

     public class responseData
     {
         public listaDatosFacturaType listaDatosFacturaType { get; set; }
     }

     public class listaDatosFacturaType
     {
         //public listaDatosFactura listaDatosFactura { get; set; }
         public List<InvaceDetails> listaDatosFactura { get; set; }
     }
    
     public class listaDatosFactura
     {
         public List<InvaceDetails> listInvaceDetails { get; set; }
     }
}
