using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusStatusFullClaro
{
    [DataContract(Name = "ConsultBonusStatusFullClaroResponse")]
    public class ConsultBonusStatusFullClaroResponse
    {
        [DataMember(Name = "MessageResponse")]
        public ConsultBonusStatusFullClaroMessageResponse MessageResponse { get; set; }
    }
}
