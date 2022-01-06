using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccount
{
    [DataContract(Name = "StatusAccountResponsePostPaid")]
    public class StatusAccountResponse
    {
        [DataMember]
        public List<StatusAccountAOC> ListStatusAccount { get; set; }

        [DataMember]
        public bool IsEnabled { get; set; }
        [DataMember]
        public bool blMessageStatusAccountVisible { get; set; }
        [DataMember]
        public string  strMessageStatusAccount { get; set; }
        
    }
}
