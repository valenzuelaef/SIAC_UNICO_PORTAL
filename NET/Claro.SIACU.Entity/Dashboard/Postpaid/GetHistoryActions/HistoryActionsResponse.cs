using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryActions
{
    [DataContract(Name = "HistoryActionsResponsePostPaid")]
    public class HistoryActionsResponse
    {
        [DataMember]
        public List<HistoryActions> ListHistoryActions { get; set; }

    }
}