using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.CancelInvoice
{
    public class CancelInvoiceRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public CancelInvoiceMessageRequest MessageRequest { get; set; }
    }
}
