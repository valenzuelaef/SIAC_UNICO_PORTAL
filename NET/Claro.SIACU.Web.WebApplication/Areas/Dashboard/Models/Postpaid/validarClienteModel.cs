using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid
{
    public class validarClienteModel
    {
        public string nombreEtiqueta { get; set; }
        public string codigoEtiqueta { get; set; }
        public string bonoElegido { get; set; }
        public string bonoLinea { get; set; }
        public string estado { get; set; }

        public string codigoRespuesta { get; set; }
        public string mensajeRespuesta { get; set; }
    }
}