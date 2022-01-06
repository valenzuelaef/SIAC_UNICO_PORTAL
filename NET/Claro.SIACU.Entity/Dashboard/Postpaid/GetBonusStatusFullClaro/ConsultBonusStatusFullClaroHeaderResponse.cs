using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusStatusFullClaro
{
    [DataContract(Name = "ConsultBonusStatusFullClaroHeaderResponse")]
    public class ConsultBonusStatusFullClaroHeaderResponse
    {
        [DataMember(Name = "HeaderResponse")]
        public Common.GetDataPower.HeaderResponse HeaderResponse { get; set; }
    }
}
