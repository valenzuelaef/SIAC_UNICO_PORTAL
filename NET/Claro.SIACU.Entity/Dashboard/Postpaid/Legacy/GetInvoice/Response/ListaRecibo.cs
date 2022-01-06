using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetInvoice.Response
{
    public class ListaRecibo
    {
        public string idonbase { get; set; }
        public string nroRecibo { get; set; }
        public string fecharegistro { get; set; }
        public string fechaemision { get; set; }
        public string fechavencimiento { get; set; }
        public string periodo { get; set; }
       
    }
}
