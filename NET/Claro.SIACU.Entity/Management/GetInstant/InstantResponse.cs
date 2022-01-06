using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetInstant
{
    [DataContract(Name = "InstantResponseManagement")]
    public class InstantResponse
    {
        [DataMember]
        public bool result { get; set; }
        [DataMember]
        public string message { get; set; }
    }
}
