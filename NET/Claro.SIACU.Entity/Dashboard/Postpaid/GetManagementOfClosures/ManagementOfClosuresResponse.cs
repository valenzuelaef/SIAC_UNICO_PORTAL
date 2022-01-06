using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetManagementOfClosures
{
    [DataContract(Name = "ManagementOfClosuresResponsePostPaid")]
    public class ManagementOfClosuresResponse
    {
        [DataMember]
        public List<Reclosures> Reclosures { get; set; }
    }
}
