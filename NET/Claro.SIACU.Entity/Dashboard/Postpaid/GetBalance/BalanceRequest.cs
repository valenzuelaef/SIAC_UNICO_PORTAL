using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBalance
{
    [DataContract(Name = "BalanceRequestPostPaid")]
    public class BalanceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string CustomerType { get; set; }
    }
}
