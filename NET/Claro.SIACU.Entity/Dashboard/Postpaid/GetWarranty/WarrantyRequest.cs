using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetWarranty
{
    [DataContract(Name = "WarrantyRequestPostPaid")]
    public class WarrantyRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CodApplication { get; set; }
        [DataMember]
        public string UserApplication { get; set; }
        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string DocumentNumber { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string Aplication { get; set; }
        
    }
}
