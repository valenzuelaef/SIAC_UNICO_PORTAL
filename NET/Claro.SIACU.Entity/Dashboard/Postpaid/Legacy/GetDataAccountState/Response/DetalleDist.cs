using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Response
{
    public class DetalleDist
    {
        public string monto { get; set; }
        public string montoDocumento { get; set; }
        public string fechaEmision { get; set; }
        public string fechaReclamo { get; set; }
        public string motivo { get; set; }
        public string nroReclamo { get; set; }
        public string reciboAsociado { get; set; }
    }
}
