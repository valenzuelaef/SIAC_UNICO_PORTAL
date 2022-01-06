using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetReceipt
{

    [DataContract(Name = "ReceiptRequestPostPaid")]
    public class ReceiptRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string plataformaAT { get; set; }
        [DataMember]
        public string csIdPub { get; set; }
        [DataMember]
        public string bmIdPub { get; set; }
        
    }
}
