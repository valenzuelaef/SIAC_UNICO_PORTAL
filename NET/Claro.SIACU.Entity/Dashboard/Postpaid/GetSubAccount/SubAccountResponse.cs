using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSubAccount
{
    [DataContract(Name = "SubAccountResponsePostPaid")]
    public class SubAccountResponse
    {
        [DataMember]
        public List<Account> ListSubAccount { get; set; }
    }
}
