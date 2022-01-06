using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.CancelInvoice
{
    public class CancelInvoiceMessageRequest
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest Header { get; set; }

        [DataMember(Name = "Body")]
        public CancelInvoiceBodyRequest Body { get; set; }
    }
}
