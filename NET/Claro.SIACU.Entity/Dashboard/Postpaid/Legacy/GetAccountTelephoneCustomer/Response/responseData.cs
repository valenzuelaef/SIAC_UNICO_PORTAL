using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Response
{
    public class responseData
    {
        public string cuenta { get; set; }
        public string customerId { get; set; }
        public string linea { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
