using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLineHistory.Response
{
    public class ListaHistoricoMotivo
    {
        public string fechaValidoDesde { get; set; }
        public string fechaRegistro { get; set; }
        public string estado { get; set; }
        public string motivo { get; set; }
        public string usuarioRegistro { get; set; }

    }
}
