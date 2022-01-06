using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "BankingMobilePrePaid")]
    public class BankingMobile
    {
        [DataMember]
        public string ICCID { get; set; }

        [DataMember]
        public string BancaMovil { get; set; }
    }
}