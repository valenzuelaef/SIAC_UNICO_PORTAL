using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "TrioPostPaid")]
    public class Trio
    {
        [DataMember]
        public string CODIGO { get; set; }
        [DataMember]
        public string DESCRIPCION { get; set; }
        [DataMember]
        public string DESCRIPCION2 { get; set; }
    }
}
