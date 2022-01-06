using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard
{
    [DbTable("TEMPO")]
    [DataContract(Name = "MembershipElectronic")]
    public class MembershipElectronic
    {
        [DbColumn("TIPO_DOCUMENTO")]
        [DataMember]
        public string TIPO_DOCUMENTO { get; set; }

        [DbColumn("NRO_DOCUMENTO")]
        [DataMember]
        public string NRO_DOCUMENTO { get; set; }

        [DbColumn("ESTADO_AFILIACION")]
        [DataMember]
        public string ESTADO_AFILIACION { get; set; }

        [DbColumn("FECHA")]
        [DataMember]
        public string FECHA { get; set; }

        [DbColumn("EMAIL")]
        [DataMember]
        public string EMAIL { get; set; }
    }
}