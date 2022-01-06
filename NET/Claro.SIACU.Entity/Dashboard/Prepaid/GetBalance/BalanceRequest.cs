using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetBalance
{
    [DataContract(Name = "BalanceRequestPrePaid")]
    public class BalanceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
    }
}
