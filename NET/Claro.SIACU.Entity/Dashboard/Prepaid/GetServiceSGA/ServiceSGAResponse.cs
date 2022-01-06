using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetServiceSGA
{
    [DataContract(Name = "ServiceSGAResponsePrepaid")]
    public class ServiceSGAResponse
    {
        [DataMember]
        public List<ServiceSGA> listServiceSGA { get; set; } 
    }
}
