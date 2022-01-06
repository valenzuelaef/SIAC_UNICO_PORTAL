using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetBalance
{
    [DataContract(Name = "BalanceResponsePrePaid")]
    public class BalanceResponse
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public List<Balance> ListBalance { get; set; }
    }
}
