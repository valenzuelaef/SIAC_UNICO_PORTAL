using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryInvoice
{
    [DataContract(Name = "HistoryInvoiceRequestPostPaid")]
    public class HistoryInvoiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerID { get; set; }  
    }
}
