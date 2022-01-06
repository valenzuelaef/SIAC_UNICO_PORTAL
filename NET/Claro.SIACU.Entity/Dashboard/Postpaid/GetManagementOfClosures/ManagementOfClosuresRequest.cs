using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetManagementOfClosures
{
    [DataContract(Name = "ManagementOfClosuresRequestPostPaid")]
    public class ManagementOfClosuresRequest : Claro.Entity.Request
    {
        [DataMember]
        public double IdCaso { get; set; }
    }
}
