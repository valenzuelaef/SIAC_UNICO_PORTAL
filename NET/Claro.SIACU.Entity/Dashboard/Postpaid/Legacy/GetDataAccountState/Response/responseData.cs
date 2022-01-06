using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Response
{
    public class responseData
    {
        public EstadoCuenta estadoCuenta { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
