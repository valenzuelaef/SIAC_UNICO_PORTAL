using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "ReasonBlockingPrePaid")]
    public class ReasonBlocking
    {
        [DataMember]
        public string MotivoBloqueo { get; set; }

        [DataMember]
        public string AlertaBloqueo { get; set; }
    }
}