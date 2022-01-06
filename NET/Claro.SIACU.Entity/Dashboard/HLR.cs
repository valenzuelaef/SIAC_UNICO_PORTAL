using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard
{
    [DataContract(Name = "HLR")]
    public class HLR
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string DescriptionError { get; set; }

        [DataMember]
        public string State { get; set; }
    }
}
