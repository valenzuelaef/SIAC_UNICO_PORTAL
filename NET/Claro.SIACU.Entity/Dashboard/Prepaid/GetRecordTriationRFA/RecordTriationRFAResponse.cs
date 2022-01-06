using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetRecordTriationRFA
{
    [DataContract(Name = "RecordTriationRFAResponse")]
    public class RecordTriationRFAResponse
    {
        [DataMember]
        public List<HistoricalTriationRFA> listHistoricalTriationRFA { get; set; }
    }
}
