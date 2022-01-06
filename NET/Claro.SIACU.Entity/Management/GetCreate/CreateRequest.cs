using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetCreate
{
    [DataContract(Name = "CreateRequestManagement")]
    public class CreateRequest : Claro.Entity.Request
    {
        [DataMember]
        public int ID_BANNER { get; set; }
        [DataMember]
        public string MENSAJE { get; set; }
        [DataMember]
        public DateTime FECHA_VIGENCIA_INICIO { get; set; }
        [DataMember]
        public DateTime FECHA_VIGENCIA_FIN { get; set; }
        [DataMember]
        public string LOGIN { get; set; }
        [DataMember]
        public int ORDEN_PRIORIDAD { get; set; }
        [DataMember]
        public DateTime DATE { get; set; }
        [DataMember]
        public string STATUS { get; set; }

    }
}
