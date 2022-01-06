using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistorySIM
{
    [DataContract(Name = "HistorySIMResponsePostPaid")]  
    public  class HistorySIMResponse
    {
        [DataMember]
        public List<HistorySIM> ListHistorySIM { get; set; }
    } 
}
