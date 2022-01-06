using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request
{
    public class request
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject(myJsonResponse); 
        public class RecursoLogico
        {
            public string numeroLinea { get; set; }

        }

        public class Producto
        {
            public RecursoLogico recursoLogico { get; set; }

        }

        public class OfertaProducto
        {
            public Producto producto { get; set; }

        }

        public class CaracteristicaAdicional
        {
            public string descripcion { get; set; }
            public string valor { get; set; }

        }

        public class Contrato
        {
            public string idContrato { get; set; }
            public OfertaProducto ofertaProducto { get; set; }
            public CaracteristicaAdicional caracteristicaAdicional { get; set; }

        }


        public Contrato contrato { get; set; }



    }
}
