using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusStatusFullClaro
{
    [DataContract(Name = "ConsultBonusStatusFullClaroBodyRequest")]
    public class ConsultBonusStatusFullClaroBodyRequest
    {
        [DataMember(Name = "coId")]
        public string coId { get; set; } 
    }
}
