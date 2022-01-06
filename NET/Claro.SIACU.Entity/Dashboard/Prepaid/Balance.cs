using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "BalancePrePaid")]
    public class Balance
    {
        [DataMember]
        public string _Balance { get; set; }
        [DataMember]
        public string Order { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Unity { get; set; }
        [DataMember]
        public string MovementTypeName { get; set; }
        [DataMember]
        public string CommercialName { get; set; }
        [DataMember]
        public string Destination { get; set; }
        [DataMember]
        public string NameCode { get; set; }
        [DataMember]
        public string Expiration { get; set; }
        [DataMember]
        public string PromotionalBonus { get; set; }
        [DataMember]
        public string ConstancyOrder { get; set; }
        [DataMember]
        public string UnityIndicator { get; set; }
        [DataMember]
        public string OtherIndicator { get; set; }

    }
}
