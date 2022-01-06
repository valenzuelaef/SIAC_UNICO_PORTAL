using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusFullClaroClient
{
    [DataContract(Name = "BonusFullClaroClientRequest")]
    public class BonusFullClaroClientRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public BonusFullClaroClientMessageRequest MessageRequest { get; set; }
    }
}
