using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Response
{
    public class responseData
    {
        public List<Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Response.listaAfiliacionDebito> listaDetalleFormaPago { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
