using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBalance
{
    [DataContract(Name = "BalanceResponsePostPaid")]
    public class BalanceResponse
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public List<Balance> Balances { get; set; }
    }
}
