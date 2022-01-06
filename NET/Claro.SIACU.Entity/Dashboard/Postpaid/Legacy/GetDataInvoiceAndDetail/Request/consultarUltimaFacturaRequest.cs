using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataInvoiceAndDetail.Request
{
    public class consultarUltimaFacturaRequest
    {
        public string codigoCliente { get; set; }
        public List<Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataInvoiceAndDetail.Common.listaOpcional> listaOpcional { get; set; }
    }
}
