using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount
{
    [DataContract(Name = "reclamoPorRecibo")]
    public class ClaimByReceipt
    {
        [DataMember(Name = "reclamo")]
        public string claim{ get; set; }

        [DataMember(Name = "creacion")]
        public string creation{ get; set; }

        [DataMember(Name = "interaccion")]
        public string interaction{ get; set; }

        [DataMember(Name = "monto")]
        public string amount{ get; set; }

        [DataMember(Name = "estado")]
        public string state{ get; set; }

        [DataMember(Name = "documento")]
        public string document { get; set; }

        [DataMember(Name = "caso")]
        public string idCase { get; set; }
    }
}
