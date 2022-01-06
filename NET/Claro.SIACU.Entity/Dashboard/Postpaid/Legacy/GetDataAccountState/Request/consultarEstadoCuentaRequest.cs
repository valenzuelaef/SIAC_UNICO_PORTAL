using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Common;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Request
{
    public class consultarEstadoCuentaRequest
    {
        public string customerId { get; set; }
        public string nroPag { get; set; }
        public string tamanoPag { get; set; }
        public string codAplicacion { get; set; }
        public string fechaComienzo { get; set; }
        public string fechaFin { get; set; }
        public string flagSoloDisputa { get; set; }
        public string flagSoloSaldo { get; set; }
        public string nroCuentaCli { get; set; }
        public string nroTelefono { get; set; }
        public string tipoConsulta { get; set; }
        public string tipoServicio { get; set; }
        public string usuario { get; set; }
        public listaOpcional listaOpcional { get; set; }
    }
}
