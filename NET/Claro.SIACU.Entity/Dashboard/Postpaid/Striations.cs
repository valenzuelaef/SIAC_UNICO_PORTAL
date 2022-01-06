using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "StriationsPostPaid")]
    public class Striations
    {
        [DataMember]
        public string TIPO_TRIADO { get; set; }
        [DataMember]
        public int NUM_TRIO { get; set; }
        [DataMember]
        public string TELEFONO { get; set; }
        [DataMember]
        public string FACTOR { get; set; }
        [DataMember]
        public string DESTINO_TRIO { get; set; }
        [DataMember]
        public string TIPO_DESTINO { get; set; }
        [DataMember]
        public string CODIGO_TIPO_DESTINO { get; set; }
        [DataMember]
        public string PORTABILIDAD { get; set; }
    }
}
