using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDebtDetail
{
    [DataContract(Name = "DebtDetailRequestPostPaid")]
    public class DebtDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public string DocumentNumber { get; set; }
    }
}