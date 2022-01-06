using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer
{
    [DataContract]
    public class responseDataProductosXCustomer
    {
        [DataMember]
        public List<ProductosXCustomer> datosHFC { get; set; }
    }
}
