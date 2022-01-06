using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetReasonCancelInvoice
{
    public class ReasonCancelInvoiceMessageRequest
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest Header { get; set; }

        [DataMember(Name = "Body")]
        public ReasonCancelInvoiceBodyRequest Body { get; set; }
    }
}
