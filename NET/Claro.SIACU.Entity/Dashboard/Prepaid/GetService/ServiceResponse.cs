using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetService
{
    [DataContract(Name = "ServiceResponsePrePaid")]
    public class ServiceResponse
    {
        [DataMember]
        public Service objService { get; set; }
    }
}
