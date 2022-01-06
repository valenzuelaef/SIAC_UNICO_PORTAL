using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.CancelInvoice
{
    public class CancelInvoiceMessageResponse
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersResponse Header { get; set; }

        [DataMember(Name = "Body")]
        public CancelInvoiceBodyResponse Body { get; set; }
    }
}
