using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.CancelInvoice
{
    public class CancelInvoiceResponse
    {
        [DataMember(Name = "MessageResponse")]
        public CancelInvoiceMessageResponse MessageResponse { get; set; }
    }
}
