using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetContractServicesToBe
{
    public class ResponseData
    {
        public List<ServiciosAsociado> serviciosAsociados { get; set; }
        public List<ListaOpcional> listaOpcional { get; set; }
    }
}
