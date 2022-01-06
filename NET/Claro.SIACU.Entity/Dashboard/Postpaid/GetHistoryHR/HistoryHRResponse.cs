using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryHR
{
    [DataContract(Name = "HistoryHRResponsePostPaid")]
    public  class HistoryHRResponse
    {
        [DataMember]
        public List<ReceiptHistory> ListHistoryHR { get; set; }
    }
}
