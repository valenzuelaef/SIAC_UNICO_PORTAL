using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetFileInvoice
{
    [DataContract(Name = "FileInvoiceRequestDashboard")]
    public class FileInvoiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Path { get; set; }
    }
}
