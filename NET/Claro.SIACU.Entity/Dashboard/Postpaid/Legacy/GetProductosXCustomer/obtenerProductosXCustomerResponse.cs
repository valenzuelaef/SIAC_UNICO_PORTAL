using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer
{
    [DataContract]
    public class obtenerProductosXCustomerResponse
    {
        [DataMember]
        public responseStatus responseStatus { get; set; }
        [DataMember]
        public responseDataProductosXCustomer responseData { get; set; }
    }
}
