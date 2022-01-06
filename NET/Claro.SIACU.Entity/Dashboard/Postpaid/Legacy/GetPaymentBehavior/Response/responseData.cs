using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentBehavior.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentBehavior.Response
{
    public class responseData
    {
        public string comportamientoPago { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
