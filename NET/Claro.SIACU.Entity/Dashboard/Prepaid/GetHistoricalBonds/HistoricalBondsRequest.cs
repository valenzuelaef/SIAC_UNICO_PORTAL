using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetHistoricalBonds
{
    [DataContract(Name = "HistoricalBondsRequest")]
    public class HistoricalBondsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string DateStart { get; set; }
        [DataMember]
        public string DateEnd { get; set; }
    }
}
