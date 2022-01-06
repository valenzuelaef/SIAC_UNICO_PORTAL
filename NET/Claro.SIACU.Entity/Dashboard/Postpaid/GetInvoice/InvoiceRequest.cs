using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetInvoice
{
    public class InvoiceRequest : Claro.Entity.Request
    {
        public string customerId { get; set; }
        public string nroRecibo { get; set; }
        public ListaOpcional listaOpcional { get; set; }
        public string cantNroRecibo { get; set; }
    }
}
