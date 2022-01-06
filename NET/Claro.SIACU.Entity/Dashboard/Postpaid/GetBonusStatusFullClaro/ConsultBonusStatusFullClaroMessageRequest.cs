using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusStatusFullClaro
{
    [DataContract(Name = "ConsultBonusStatusFullClaroMessageRequest")]
    public class ConsultBonusStatusFullClaroMessageRequest
    {
        [DataMember(Name = "Header")]
        public ConsultBonusStatusFullClaroHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultBonusStatusFullClaroBodyRequest Body { get; set; }
    }
}
