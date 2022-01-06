using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    public class InvaceDetails
    {
        public string numOrden { get; set; }
        public string numFactura { get; set; }
        public string nombreContacto { get; set; }
        public string nombreEmpleado { get; set; }
        public string lineaCuatro { get; set; }
        public string lineaCinco { get; set; }
        public string distrito { get; set; }
        public string provincia { get; set; }
        public string departamento { get; set; }
        public string nroDocumento { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string fechaEmisión { get; set; }
        public string fechaVencimiento { get; set; }
        public string codCliente { get; set; }
        public string nroCiclo { get; set; }
        public string periodo { get; set; }
        public string montoTotalTax { get; set; }
        public string montoTotalNet { get; set; }
        public string montoTotal { get; set; }
        public string mes { get; set; }
        public string anio { get; set; }

    }
}
