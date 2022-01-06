using System.Runtime.Serialization;
using Claro.SIACU.Entity.File;

namespace Claro.SIACU.Entity.Dashboard.Board.GetInvoiceFtp
{
    [DataContract(Name = "InvoiceFtpResponseDashboard")]

    public class InvoiceFtpResponse
    {
        [DataMember]
        public GlobalDocument ObjGlobalDocument { get; set; }
    }
}
