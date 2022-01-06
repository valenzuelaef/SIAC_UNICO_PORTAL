using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetInvoice.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetInvoice.Response
{
    [DataContract]
    public class response
    {
        [DataMember]
        public ObtenerIdOnbaseResponse obtenerIdOnbaseResponse { get; set; }
    }
}
