using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetInstant
{
    [DataContract(Name = "InstantRequestPrepaid")]
    public class InstantRequest : Claro.Entity.Request
    {
        [DataMember]
        public string DataSearch { get; set; }
        [DataMember]
        public string TypeSearch { get; set; }
    }
}
