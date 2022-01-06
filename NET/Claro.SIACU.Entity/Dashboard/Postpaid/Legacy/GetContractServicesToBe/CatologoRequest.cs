using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetContractServicesToBe
{
    [DataContract]
    public class CatalogoResponse
    {
        [DataMember]
        public ObtenerServiciosPlanPorContratoResponse obtenerServiciosPlanPorContratoResponse { get; set; }
    }
}
