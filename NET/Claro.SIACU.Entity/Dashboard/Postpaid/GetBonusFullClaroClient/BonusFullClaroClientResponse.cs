using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusFullClaroClient
{
    [DataContract(Name = "BonusFullClaroClientResponse")]
    public class BonusFullClaroClientResponse
    {
        [DataMember(Name = "MessageResponse")]
        public BonusFullClaroClientMessageResponse MessageResponse { get; set; }
    }
}
