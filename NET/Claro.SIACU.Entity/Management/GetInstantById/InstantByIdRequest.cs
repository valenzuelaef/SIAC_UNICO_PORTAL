using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetInstantById
{
    [DataContract(Name = "InstantByIdRequestManagement")]
    public class InstantByIdRequest : Claro.Entity.Request
    {
        [DataMember]
        public int IdInstant { get; set; }
    }
}
