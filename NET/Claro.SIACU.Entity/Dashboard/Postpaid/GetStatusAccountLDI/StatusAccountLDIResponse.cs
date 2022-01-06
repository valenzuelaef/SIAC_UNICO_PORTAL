using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccountLDI
{

    [DataContract(Name = "StatusAccountLDIResponsePostPaid")]
    public class StatusAccountLDIResponse
    {
        [DataMember]
        public List<StatusAccountLDI> Bills { get; set; }

        [DataMember]
        public List<StatusAccountLDI> Payments { get; set; }

    }
}
