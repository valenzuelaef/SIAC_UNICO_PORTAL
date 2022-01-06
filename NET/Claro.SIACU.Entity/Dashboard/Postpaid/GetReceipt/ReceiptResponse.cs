using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetReceipt
{
    [DataContract(Name = "ReceiptResponsePostPaid")]
    public class ReceiptResponse
    {
        [DataMember]
        public Entity.Dashboard.Postpaid.Receipt ObjReceipt { get; set; }
        [DataMember]
        public bool isEnvioEmail { get; set; }
    } 
}
