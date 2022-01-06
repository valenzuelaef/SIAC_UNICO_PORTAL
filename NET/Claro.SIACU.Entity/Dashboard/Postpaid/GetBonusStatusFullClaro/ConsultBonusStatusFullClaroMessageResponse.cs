using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusStatusFullClaro
{
    [DataContract(Name = "ConsultBonusStatusFullClaroMessageResponse")]
    public class ConsultBonusStatusFullClaroMessageResponse
    {
        [DataMember(Name = "Header")]
        public ConsultBonusStatusFullClaroHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultBonusStatusFullClaroBodyResponse Body { get; set; }
    }
}
