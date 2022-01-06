using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ServiceBSCSPostPaid")]
    public class ServiceBSCS
    {
        [DataMember]
        public string SERVICIO { get; set; }
        [DataMember]
        public string PAQUETE { get; set; }
        [DataMember]
        public string ESTADO { get; set; }
    }
}
