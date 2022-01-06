using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "BalancePostPaid")]
    public class Balance
    {
        [DataMember]
        public long BALANCE_UNITS { get; set; }
        [DataMember]
        public string CREDIT_DESCRIPTION { get; set; }
        [DataMember]
        public byte CREDIT_TYPE { get; set; }
        [DataMember]
        public string EXPIRY_DATE { get; set; }
        [DataMember]
        public long MINIMUM_BALANCE { get; set; }
        [DataMember]
        public string TARIF_DESCRIPTION { get; set; }
        [DataMember]
        public ulong TARIF_ID { get; set; }
        [DataMember]
        public string UNIT_DESCRIPTION { get; set; }
        [DataMember]
        public string WALLET_DESCRIPTION { get; set; }
        [DataMember]
        public long WALLET_ID { get; set; }
        [DataMember]
        public string WALLET_SHORT_DESCRIPTION { get; set; }
        [DataMember]
        public string CONSUMO { get; set; }
        [DataMember]
        public string BALANCE { get; set; }
    }
}
