using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetSalesData
{
    [DataContract(Name = "SalesDataRequest")]
    public class SalesDataRequest : Claro.Entity.Request
    {
        [DataMember]
        public string TransactionID { get; set; }
        [DataMember]
        public string ApplicationID { get; set; }
        [DataMember]
        public string ApplicationName { get; set; }
        [DataMember]
        public string ApplicationUser { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Matter { get; set; }
        [DataMember]
        public string IssueSeries { get; set; }
    }
}
