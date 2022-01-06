using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetParamaterClarify.GetDescriptions
{
    public class GetDescriptionsRequest : Claro.Entity.Request
    {
       
        [DataMember]
        public string tipoProceso { get; set; }
    }
}
