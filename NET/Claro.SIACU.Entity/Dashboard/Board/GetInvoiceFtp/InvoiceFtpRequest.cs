using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetInvoiceFtp
{
    [DataContract(Name = "InvoiceFtpRequestDashboard")]
    public class InvoiceFtpRequest : Claro.Entity.Request
    {

        [DataMember]
        public string filePath { get; set; }
        [DataMember]
        public string fileName { get; set; }
    }
}
