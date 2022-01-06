using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTriaciones
{
    [DataContract(Name = "StriationsResponsePostPaid")]
    public class StriationsResponse
    {
        [DataMember]
        public List<Striations> Striations { get; set; }
    }
}
