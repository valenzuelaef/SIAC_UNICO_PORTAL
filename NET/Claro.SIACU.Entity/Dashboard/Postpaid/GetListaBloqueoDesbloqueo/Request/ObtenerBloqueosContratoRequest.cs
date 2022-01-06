using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetListaBloqueoDesbloqueo.Request
{
   public class ObtenerBloqueosContratoRequest
    {
        public string coIdPub { get; set; }
        public string telefono { get; set; }
        public string codigoRazon { get; set; }
        public List<ListaOpcional> listaOpcional { get; set; }
    }
   public class ListaOpcional
   {
       public string clave { get; set; }
       public string valor { get; set; }
   }
}
