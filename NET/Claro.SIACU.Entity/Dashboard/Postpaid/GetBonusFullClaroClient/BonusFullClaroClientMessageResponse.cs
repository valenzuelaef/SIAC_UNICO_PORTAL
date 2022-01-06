using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusFullClaroClient
{
    [DataContract(Name = "BonusFullClaroClientMessageResponse")]
    public class BonusFullClaroClientMessageResponse
    {
        [DataMember(Name = "Header")]
        public BonusFullClaroClientHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public BonusFullClaroClientBodyResponse Body { get; set; }
    }
}
