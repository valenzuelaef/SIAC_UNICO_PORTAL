using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCreditLimit
{
    [DataContract(Name = "CreditLimitRequestPostPaid")]
    public class CreditLimitRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string TypeConsultationOAC { get; set; }
        [DataMember]
        public string TypeConsultationDetailOAC { get; set; }
        [DataMember]
        public string Period { get; set; }
        [DataMember]
        public string Application { get; set; }


    }
}
