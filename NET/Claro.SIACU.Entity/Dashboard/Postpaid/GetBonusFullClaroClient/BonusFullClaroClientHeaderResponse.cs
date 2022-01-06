using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusFullClaroClient
{
    [DataContract(Name = "BonusFullClaroClientHeaderResponse")]
    public class BonusFullClaroClientHeaderResponse
    {
        [DataMember(Name = "HeaderResponse")]
        public Common.GetDataPower.HeaderResponse HeaderResponse { get; set; }
    }
}
