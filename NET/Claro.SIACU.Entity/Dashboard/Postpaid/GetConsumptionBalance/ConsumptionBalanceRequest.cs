using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumptionBalance
{
    [DataContract(Name = "ConsumptionBalanceRequestPostPaid")]
    public class ConsumptionBalanceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
    }
}
