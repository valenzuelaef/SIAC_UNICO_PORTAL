using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.CancelInvoice
{

    public class TypificationRequest : Claro.Entity.Request
    {
        [DataMember]
        public string TRANSACTION_NAME { get; set; }

        [DataMember]
        public string TELEPHONE_NUMBER { get; set; } 
    }
}
