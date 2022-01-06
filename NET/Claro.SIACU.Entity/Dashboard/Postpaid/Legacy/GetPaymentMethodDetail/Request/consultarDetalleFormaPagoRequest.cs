using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Request
{
    public class consultarDetalleFormaPagoRequest
    {
        public string customerId { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}