using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetReasonCancelInvoice
{
    public class ReasonCancelInvoiceResponse
    {
        [DataMember(Name = "MessageResponse")]
        public ReasonCancelInvoiceMessageResponse MessageResponse { get; set; }
    }
}
