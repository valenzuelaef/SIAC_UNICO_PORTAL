using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricDelivery
{
    [DataContract(Name = "HistoricDeliveryResponsePostpaid")]
    public class HistoricDeliveryResponse
    {
        [DataMember]
        public List<HistoricDelivery> ListHistoricDelivery { get; set; }

        [DataMember]
        public List<ReceiptHistory> LisReceiptHistory { get; set; }
    }
}
