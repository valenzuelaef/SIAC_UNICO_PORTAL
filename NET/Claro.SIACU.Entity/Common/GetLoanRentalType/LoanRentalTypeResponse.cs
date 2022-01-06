using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetLoanRentalType
{
    [DataContract(Name = "LoanRentalResponseCommon")]
    public class LoanRentalResponse
    {
        [DataMember]
        public List<ListItem> ListItem { get; set; }
    }
}
