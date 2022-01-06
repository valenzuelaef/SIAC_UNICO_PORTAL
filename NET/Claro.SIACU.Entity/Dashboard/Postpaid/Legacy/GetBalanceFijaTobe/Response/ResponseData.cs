using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceFijaTobe.Response
{
    public class ResponseData
    {
        public List<ListaBolsa> listaBolsa { get; set; }
        public List<ListaOpcional> listaOpcional { get; set; }
    }
}
