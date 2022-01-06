using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard
{
    [DataContract(Name = "Segment")]
    public class Segment
    {
        [DataMember]
        public string ULTIMAACTUALIZACION { get; set; }

        [DataMember]
        public string SEGMENTO { get; set; }

        [DataMember]
        public string NRO_DOC { get; set; }
    }
}