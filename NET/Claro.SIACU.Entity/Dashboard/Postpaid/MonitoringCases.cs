using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "MonitoringCasesPostPaid")]
    public class MonitoringCases
    {
        [Data.DbColumn("IDCASO")]
        [DataMember]
        public string IDCASO { get; set; }

        [Data.DbColumn("FECREGISTRO")]
        [DataMember]
        public string FECREGISTRO { get; set; }

        [Data.DbColumn("CLIENTE")]
        [DataMember]
        public string CLIENTE { get; set; }

        [Data.DbColumn("TIPOCLIENTE")]
        [DataMember]
        public string TIPOCLIENTE { get; set; }

        [Data.DbColumn("SEGMENTO")]
        [DataMember]
        public string SEGMENTO { get; set; }

        [Data.DbColumn("ESTADO")]
        [DataMember]
        public string ESTADO { get; set; }

        [Data.DbColumn("PRIORIDAD")]
        [DataMember]
        public string PRIORIDAD { get; set; }

        [Data.DbColumn("TIPO")]
        [DataMember]
        public string TIPO { get; set; }

        [Data.DbColumn("CLASE")]
        [DataMember]
        public string CLASE { get; set; }

        [Data.DbColumn("SUBCLASE")]
        [DataMember]
        public string SUBCLASE { get; set; }

        [Data.DbColumn("MONTO")]
        [DataMember]
        public string MONTO { get; set; }

        [Data.DbColumn("DUENO")]
        [DataMember]
        public string DUENO { get; set; }

        [Data.DbColumn("NOMBREDUENO")]
        [DataMember]
        public string NOMBREDUENO { get; set; }

        [Data.DbColumn("RECURRENCIA")]
        [DataMember]
        public string RECURRENCIA { get; set; }

        [Data.DbColumn("CASOCLARIFY")]
        [DataMember]
        public string CASOCLARIFY { get; set; }

        [Data.DbColumn("TIEMPOTRANSCURRIDO")]
        [DataMember]
        public string TIEMPOTRANSCURRIDO { get; set; }

        [Data.DbColumn("FECHAREGISTRO")]
        [DataMember]
        public string FECHAREGISTRO { get; set; }
    }
}