using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethod.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethod.Request
{
    public class consultarFormaPagoRequest
    {
        public string customerId { get; set; }
        public string linea { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
