using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentBehavior.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentBehavior.Request
{
    public class consultarComportamientoPagoHistRequest
    {
        public string customerId { get; set; }
        public string tipoDocumento { get; set; }
        public string nroDocumento { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
