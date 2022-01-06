using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetService
{
    [DataContract(Name = "ServiceRequestPrePaid")]
    public class ServiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public Entity.Dashboard.Prepaid.Service objService { get; set; }
        [DataMember]
        public string TransactionID { get; set; }
        [DataMember]
        public string ApplicationID { get; set; }
        [DataMember]
        public string ApplicationName { get; set; }
        [DataMember]
        public string ApplicationUser { get; set; }
        [DataMember]
        public string Matter { get; set; }
        [DataMember]
        public string IssueSeries { get; set; }
    }
}
