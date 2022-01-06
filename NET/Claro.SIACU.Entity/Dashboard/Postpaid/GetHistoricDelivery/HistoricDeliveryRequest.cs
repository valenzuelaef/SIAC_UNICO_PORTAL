using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricDelivery
{
    [DataContract(Name = "HistoricDeliveryRequestPostPaid")]
    public class HistoricDeliveryRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strReceipt { get; set; }

        [DataMember]
        public string strFlag { get; set; }

        [DataMember]
        public string strCustomer { get; set; }

        [DataMember]
        public string flagPlataforma { get; set; }
    }
}
