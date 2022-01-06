using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetReasonCancelInvoice
{
    public class ReasonCancelInvoiceRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public ReasonCancelInvoiceMessageRequest MessageRequest { get; set; }
    }
}
