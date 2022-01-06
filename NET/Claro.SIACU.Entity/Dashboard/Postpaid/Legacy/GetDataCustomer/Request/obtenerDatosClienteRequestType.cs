using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataCustomer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataCustomer.Request
{
    public class obtenerDatosClienteRequestType
    {
        public string custCode { get; set; }
        public string dnNum { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }

}


