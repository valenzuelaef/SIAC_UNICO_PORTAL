using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetDeactivate
{
    [DataContract(Name = "DeactivateRequestManagement")]
    public class DeactivateRequest : Claro.Entity.Request
    {
        [DataMember]
        public int ID_BANNER { get; set; }
        [DataMember]
        public string LOGIN { get; set; }
        [DataMember]
        public DateTime DATE { get; set; }
        [DataMember]
        public string STATUS { get; set; }

    }
}
