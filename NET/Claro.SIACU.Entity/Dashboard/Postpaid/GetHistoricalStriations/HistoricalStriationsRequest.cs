using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricalStriations
{
    [DataContract(Name = "HistoricalStriationsRequestPostPaid")]
    public class HistoricalStriationsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string ContractID { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string Customer { get; set; }
    }
}
