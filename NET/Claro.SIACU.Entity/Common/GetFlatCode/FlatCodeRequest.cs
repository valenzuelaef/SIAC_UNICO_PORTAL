using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetFlatCode
{
    [DataContract]
    public class FlatCodeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string ContractId { get; set; }
    }
}
