using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLineHistory.Common;


namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLineHistory.Request
{ 
        public class consultarHistoricoLineaRequest
        {
            public string coid { get; set; }

            public listaOpcional listaOpcional { get; set; }

        }
    
}
