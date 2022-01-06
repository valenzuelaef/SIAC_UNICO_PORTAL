using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetScheduledTransaction
{
    [DataContract(Name="ScheduledTransactionResponsePostPaid")]
    public class ScheduledTransactionResponse
    {
        [DataMember]
        public List<ScheduledTransaction> ScheduledTransactions { get; set; }
    }
}
