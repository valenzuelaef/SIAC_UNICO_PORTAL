using System.Runtime.Serialization;

namespace Claro.SIACU.Entity
{
    [DataContract(Name = "ListItem")]
    public class ListItem
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Code2 { get; set; }
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int number { get; set; }  

    }
}