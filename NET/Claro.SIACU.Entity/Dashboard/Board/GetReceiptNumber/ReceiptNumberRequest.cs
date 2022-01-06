using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetReceiptNumber
{

    [DataContract(Name = "ReceiptNumberRequestDashboard")]
    public class ReceiptNumberRequest : Claro.Entity.Request
    {
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string EmissionDate { get; set; }
    }
}
