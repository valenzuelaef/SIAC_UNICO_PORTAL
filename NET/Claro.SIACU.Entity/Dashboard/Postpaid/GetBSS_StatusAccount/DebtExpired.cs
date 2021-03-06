using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount
{
    [DataContract(Name = "deudaVencida")]
    public class DebtExpired
    {
        public DebtExpired()
        {
            StateAccount = new List<DetailStateAccountCab>();
        }

        [DataMember(Name = "audit")]
        public AuditDebt audit { get; set; }
        
        [DataMember(Name = "xEstadoCuenta")]
        public List<DetailStateAccountCab> StateAccount { get; set; }
    }
}
