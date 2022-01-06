using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusFullClaroClient
{
    [DataContract(Name = "BonusFullClaroClientBodyRequest")]
    public class BonusFullClaroClientBodyRequest
    {
        [DataMember(Name = "nroDocumento")]
        public string nroDocumento { get; set; }

        [DataMember(Name = "tipoDocumento")]
        public string tipoDocumento { get; set; } 
    }
}
