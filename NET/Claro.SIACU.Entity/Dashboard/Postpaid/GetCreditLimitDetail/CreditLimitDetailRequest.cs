using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCreditLimitDetail
{
    [DataContract(Name = "CreditLimitDetailRequestPostPaid")]
    public class CreditLimitDetailRequest : Claro.Entity.Request
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
        public string Account { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public string NumberPac { get; set; }      
    }
}
