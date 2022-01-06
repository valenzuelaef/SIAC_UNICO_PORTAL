using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard
{
    [DataContract(Name = "OptionalObject")]
    public class OptionalObject
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string value { get; set; }
    }
}