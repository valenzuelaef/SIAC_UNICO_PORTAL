using System.Runtime.Serialization;
using Claro.SIACU.Entity.File;

namespace Claro.SIACU.Entity.Dashboard.Board.GetFileInvoice
{
    [DataContract(Name = "FileInvoiceResponseDashboard")]
    public class FileInvoiceResponse
    {
        [DataMember]
        public GlobalDocument ObjGlobalDocument { get; set; }

        [DataMember]
        public byte[] ObjArrBuffer { get; set; }


    }
}
