using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumptionBalance
{
    [DataContract(Name = "ConsumptionBalanceResponsePostPaid")]
    public class ConsumptionBalanceResponse
    {
        [DataMember]
        public List<Recharge> ListRefills { get; set; }
    }
}
