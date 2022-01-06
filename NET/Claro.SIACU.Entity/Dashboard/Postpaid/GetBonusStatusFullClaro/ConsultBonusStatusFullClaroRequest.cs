using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusStatusFullClaro
{
    [DataContract(Name = "ConsultBonusStatusFullClaroRequest")]
    public class ConsultBonusStatusFullClaroRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public ConsultBonusStatusFullClaroMessageRequest MessageRequest { get; set; }
    }
}
