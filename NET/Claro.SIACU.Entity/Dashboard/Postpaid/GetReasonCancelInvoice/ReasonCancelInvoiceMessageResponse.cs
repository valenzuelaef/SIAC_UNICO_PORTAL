using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetReasonCancelInvoice
{
    public class ReasonCancelInvoiceMessageResponse
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersResponse Header { get; set; }

        [DataMember(Name = "Body")]
        public ReasonCancelInvoiceBodyResponse Body { get; set; }
    }
}
