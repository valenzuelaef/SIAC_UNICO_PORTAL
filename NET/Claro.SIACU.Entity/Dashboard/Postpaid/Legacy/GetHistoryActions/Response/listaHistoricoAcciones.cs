using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Response
{
    public class listaHistoricoAcciones
    {
        public int contrato { get; set; }
        public string servicio { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaOrden { get; set; }
        public string hora { get; set; }
        public string fecha { get; set; }
        public string usuario { get; set; }
    }
}