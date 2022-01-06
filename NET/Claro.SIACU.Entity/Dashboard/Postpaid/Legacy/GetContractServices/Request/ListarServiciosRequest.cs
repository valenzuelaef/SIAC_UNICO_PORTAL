using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetContractServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetContractServices.Request
{
    public class ListarServiciosRequest
    {
        public string contractIdPub { get; set; }
        public ListaOpcional listaOpcional { get; set; }
    }
}
