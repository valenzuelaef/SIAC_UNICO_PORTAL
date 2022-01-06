using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCreditLimitDetail
{
    [DataContract(Name = "CreditLimitDetailResponsePostPaid")]
    public class CreditLimitDetailResponse
    {
        [DataMember]
        public CreditLimitDetail CreditLimitDetail { get; set; }
    }
}
