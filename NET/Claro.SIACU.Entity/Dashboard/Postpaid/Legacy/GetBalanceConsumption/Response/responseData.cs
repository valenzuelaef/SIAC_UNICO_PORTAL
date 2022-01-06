using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Response
{
    public class responseData
    {
        public List<listaBolsa> listaBolsa { get; set; }
        public List<listaOpcional> ListaOpcional { get; set; }
    }
}
