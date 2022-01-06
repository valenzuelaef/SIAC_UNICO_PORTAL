using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusStatusFullClaro
{
    [DataContract(Name = "ConsultBonusStatusFullClaroHeaderRequest")]
    public class ConsultBonusStatusFullClaroHeaderRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public Common.GetDataPower.HeaderRequest HeaderRequest { get; set; }
    }
}
