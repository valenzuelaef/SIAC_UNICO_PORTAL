using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer
{
    [DataContract]
    public class obtenerProductosXCustomerRequest : Claro.Entity.Request
    {
        [DataMember]
        public string idCustomer { get; set; }
    }
}
