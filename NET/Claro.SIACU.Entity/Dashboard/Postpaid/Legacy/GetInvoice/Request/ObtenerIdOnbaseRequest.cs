using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetInvoice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetInvoice.Request
{
    [DataContract]
   public class ObtenerIdOnbaseRequest
    {
         [DataMember]
        public string customerId { get; set; }
         [DataMember]
        public string nroRecibo { get; set; }
         [DataMember]
         public string cantidadRecibos { get; set; }
         [DataMember]
        public ListaOpcional listaOpcional { get; set; }
    }
}
