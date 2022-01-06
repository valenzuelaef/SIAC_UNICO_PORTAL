using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataHistory.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataHistory.Response
{
    public class ResponseData
    {
        public List<ListaHistoricoDato> listaHistoricoDatos { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
