using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "BalanceBagPrepaid")]
    public class BalanceBag
    {
        [DataMember]
        public string Account { get; set; }

        [DataMember]
        public string BalanceUnm { get; set; }

        [DataMember]
        public string DateExpiration { get; set; }
    }
}