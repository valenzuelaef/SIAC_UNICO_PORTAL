using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPetitions.Response
{
    public class ListaPeticiones
    {
        public string fechaPeticion { get; set; }
        public string fechaVencimiento { get; set; }
        public string estadoPeticion { get; set; }
        public string accionSolicitada { get; set; }
        public string usuario { get; set; }
        public string ordenId { get; set; }
    }
}
