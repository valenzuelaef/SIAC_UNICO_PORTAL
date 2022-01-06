using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCreditLimit
{
    [DataContract(Name = "CreditLimitResponsePostPaid")]
    public class CreditLimitResponse
    {
        [DataMember]
         public List<CreditLimit> ListCreditLimit { get; set; }


    }
}
