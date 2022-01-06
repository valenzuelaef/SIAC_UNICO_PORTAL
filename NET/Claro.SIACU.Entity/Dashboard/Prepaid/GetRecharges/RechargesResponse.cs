using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetRecharges
{
    [DataContract(Name = "RechargesResponsePrePaid")]
    public class RechargesResponse
    {
        [DataMember]
        public List<Recharge> listRecharge { get; set; }
    }
}
