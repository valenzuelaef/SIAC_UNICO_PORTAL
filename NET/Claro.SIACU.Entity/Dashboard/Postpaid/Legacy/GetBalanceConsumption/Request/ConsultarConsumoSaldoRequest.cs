using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Request
{
    public class consultarSaldoPostpagoRequest
    {
        public string msisdn { get; set; }
        public string coIdPub { get; set; }
        public ListaOpcional listaOpcional { get; set; }
    }
}
