using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetTypeProduct.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetTypeProduct.Response
{
    public class responseData
    {
        public string lineaContrato { get; set; }
        public List<listaTipoProducto> listaTipoProducto { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
