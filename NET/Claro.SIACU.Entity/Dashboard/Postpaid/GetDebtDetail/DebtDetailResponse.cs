using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDebtDetail
{
    [DataContract(Name = "DebtDetailResponsePostPaid")]
    public class DebtDetailResponse
    {
        [DataMember]
        public List<ApadeceDebt> ListDebtDetail { get; set; }
    }
}

