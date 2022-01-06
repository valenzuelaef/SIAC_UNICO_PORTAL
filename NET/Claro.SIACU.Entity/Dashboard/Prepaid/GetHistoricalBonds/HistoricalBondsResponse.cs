using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetHistoricalBonds
{
    [DataContract(Name = "HistoricalBondsResponse")]
    public class HistoricalBondsResponse
    {
        [DataMember]
        public List<HistoricalBonds> listHistoricalBonds { get; set; }
    }
}
