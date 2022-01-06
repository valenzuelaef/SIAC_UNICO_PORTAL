using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryInvoice
{

    [DataContract(Name = "HistoryInvoiceResponsePostPaid")]
    public class HistoryInvoiceResponse
    {
        [DataMember]
        public List<ReceiptHistory> ListReceiptHistory { get; set; }
    }
}
