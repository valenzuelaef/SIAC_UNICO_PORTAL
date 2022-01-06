using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DbTable("TEMPO")]
    [DataContract(Name = "MotiveLowPrePaid")]
    public class MotiveLow
    {
        [DbColumn("BLINI_COD")]
        [DataMember]
        public string Codigo { get; set; }

        [DbColumn("BLINI_CODCLI")]
        [DataMember]
        public string CodCli { get; set; }

        [DbColumn("BLINV_PHONE")]
        [DataMember]
        public string Phone { get; set; }

        [DbColumn("BLIND_FEC_REG")]
        [DataMember]
        public string Fecha { get; set; }

        [DbColumn("BLIND_FEC_BAJ")]
        [DataMember]
        public string FechaBaja { get; set; }

        [DbColumn("BLINV_MOT")]
        [DataMember]
        public string Motivo { get; set; }

        [DbColumn("BLINV_EST")]
        [DataMember]
        public string Estado { get; set; }
    }
}