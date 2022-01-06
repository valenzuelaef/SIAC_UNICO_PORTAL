using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusFullClaroClient
{
    [DataContract(Name = "BonusFullClaroClientMessageRequest")]
    public class BonusFullClaroClientMessageRequest
    {
        [DataMember(Name = "Header")]
        public BonusFullClaroClientHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public BonusFullClaroClientBodyRequest Body { get; set; }
    }
}
