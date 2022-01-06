using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer
{
    [DataContract]
    public class ProductosXCustomer
    {
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string planTarifario { get; set; }
        [DataMember]
        public string direccionInstalacion { get; set; }
        [DataMember]
        public string combo { get; set; }
        [DataMember]
        public string descuento { get; set; }
        [DataMember]
        public string telefonia { get; set; }
        [DataMember]
        public string cable { get; set; }
        [DataMember]
        public string internet { get; set; }
    }
}
