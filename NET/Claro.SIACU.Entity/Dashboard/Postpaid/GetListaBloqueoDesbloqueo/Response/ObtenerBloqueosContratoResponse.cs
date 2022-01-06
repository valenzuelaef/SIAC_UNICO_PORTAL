using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetListaBloqueoDesbloqueo.Response
{
   public class ObtenerBloqueosContratoResponse
    {
        public ResponseAudit responseAudit { get; set; }
        public ResponseData responseData { get; set; }
    }

   public class ResponseAudit
   {
       public string idTransaccion { get; set; }
       public string codigoRespuesta { get; set; }
       public string mensajeRespuesta { get; set; }
   }

   public class ListaBloqueo
   {
       public string ticklerNumber { get; set; }
       public string ticklerCode { get; set; }
       public string ticklerStatus { get; set; }
       public string createdBy { get; set; }
       public string createdDate { get; set; }
       public string longDescription { get; set; }
       public string estado { get; set; }
   }

   public class ResponseData
   {
       public IList<ListaBloqueo> listaBloqueo { get; set; }
       public object listaOpcional { get; set; }
   }






}
