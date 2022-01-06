using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Common;


namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Response
{
    public class consultarDetalleFormaPagoResponse
    {
        public responseAudit responseAudit { get; set; }
        public responseData responseData { get; set; }
    }
}