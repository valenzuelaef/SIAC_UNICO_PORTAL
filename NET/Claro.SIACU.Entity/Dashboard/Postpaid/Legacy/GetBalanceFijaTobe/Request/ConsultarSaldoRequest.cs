using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceFijaTobe.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceFijaTobe.Request
{
    public class ConsultarSaldoRequest
    {
        public string msisdn { get; set; }
        public string coIdPub { get; set; }
        public List<ListaOpcional> listaOpcional { get; set; }
    }
}
