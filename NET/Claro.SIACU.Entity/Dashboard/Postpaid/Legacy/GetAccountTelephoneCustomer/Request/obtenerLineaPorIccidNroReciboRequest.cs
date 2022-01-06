using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request
{
    public class obtenerLineaPorIccidNroReciboRequest
    {

        public string customerId { get; set; }
        public string nroRecibo { get; set; }
        public string iccid { get; set; }
       
    }
}
