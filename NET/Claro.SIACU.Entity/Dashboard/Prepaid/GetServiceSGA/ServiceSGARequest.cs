using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetServiceSGA
{
    [DataContract(Name = "ServiceSGARequestPrepaid")]
    public class ServiceSGARequest : Claro.Entity.Request
    {
        [DataMember]
        public string CodigoRecarga { get; set; }
    }
}
