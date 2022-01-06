using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethod.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethod.Response
{
    public class responseData
    {
        public string formaDePago { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
