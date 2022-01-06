using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetInvoice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetInvoice.Response
{
    public class ResponseData
    {
        public List<ListaOpcional> listaOpcional { get; set; }
        public List<ListaRecibo> listaRecibos { get; set; }
    }
}
