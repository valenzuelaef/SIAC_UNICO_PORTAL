using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetScheduledTransaction
{
    [DataContract(Name="ScheduledTransactionRequestPostPaid")]
    public class ScheduledTransactionRequest: Claro.Entity.Request
    {
        [DataMember]
        public string IdContract { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Adviser { get; set; }
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string TransactionType { get; set; }
        [DataMember]
        public string InteractionCode { get; set; }
        [DataMember]
        public string CacDac { get; set; }
    }
}
