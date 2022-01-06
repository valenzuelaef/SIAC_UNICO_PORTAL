using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetPortability
{
    [DataContract(Name = "PortabilityRequestCommon")]
    public class PortabilityRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
    }
}