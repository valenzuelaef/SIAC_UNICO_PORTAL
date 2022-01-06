using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricalStriations
{
    [DataContract(Name = "HistoricalStriationsResponsePostpaid")]
    public class HistoricalStriationsResponse
    {
        [DataMember]
        public List<HistoricalStriations> HistoricalStriations { get; set; }
    }
}
