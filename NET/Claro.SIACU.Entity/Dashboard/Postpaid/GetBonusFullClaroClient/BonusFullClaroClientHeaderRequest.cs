using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusFullClaroClient
{
    [DataContract(Name = "BonusFullClaroClientHeaderRequest")]
    public class BonusFullClaroClientHeaderRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public Common.GetDataPower.HeaderRequest HeaderRequest { get; set; }
    }
}
