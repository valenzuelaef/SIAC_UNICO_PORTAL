using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetFlatCode
{
    [DataContract]
    public class FlatCodeResponse
    {
        [DataMember]
        public string FlatCode { get; set; }
    }
}
