using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccountAOC
{
    [DataContract(Name = "StatusAccountAOCResponsePostPaid")]
    public class StatusAccountAOCResponse
    {
        [DataMember]
        public List<StatusAccountAOC> ListStatusAccountAOC { get; set; }
    }
}
