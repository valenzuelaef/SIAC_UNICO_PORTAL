using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Dashboard.Postpaid;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetInvoceDetails
{
    public class InvoceDetailsRequest
    {
        public obtenerDatosFacturaRequest obtenerDatosFacturaRequest { get; set; }
    }

    public class obtenerDatosFacturaRequest
    {
        public string csId { get; set; }
        public string periodo { get; set; }

    }
}
