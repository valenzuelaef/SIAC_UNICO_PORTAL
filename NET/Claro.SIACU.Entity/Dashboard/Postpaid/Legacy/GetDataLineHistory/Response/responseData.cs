using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLineHistory.Common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLineHistory.Response
{
    public class responseData
    {
        public List<ListaHistoricoMotivo> listaHistoricoMotivo { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }

    }
}
