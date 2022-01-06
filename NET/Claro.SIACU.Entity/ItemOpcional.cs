using System.Runtime.Serialization;

namespace Claro.SIACU.Entity
{
    [DataContract(Name = "ItemOpcional")]
    public class ItemOpcional
    {
        [DataMember]
        public string campo { get; set; }
        [DataMember]
        public string valor { get; set; }
    }
}
