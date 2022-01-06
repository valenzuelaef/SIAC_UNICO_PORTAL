using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "HistorySIMPostPaid")]
   [Data.DbTable("TEMPO")]
    public class HistorySIM
    {
        [DataMember]
        [Data.DbColumn("ESTADO")]
        public string Estado { get; set; }
        [DataMember]
        [Data.DbColumn("MOTIVO")]
        public string Motivo { get; set; }
        [DataMember]
        [Data.DbColumn("VALIDO_DESDE")]
        public string Valido_Desde { get; set; }
        [DataMember]
        [Data.DbColumn("IMSI")]
        public string IMSI { get; set; }
        [DataMember]
        [Data.DbColumn("ICCID")]
        public string ICCID { get; set; }
        [DataMember]
        [Data.DbColumn("INTRODUCIDO_AL")]
        public string IntroducidoAl { get; set; }
        [DataMember]
        [Data.DbColumn("MODIFICADO")]
        public string Modificado { get; set; }
        [DataMember]
        [Data.DbColumn("INTRODUCIDO_POR")]
        public string IntroducidoPor { get; set; }

        [DataMember]
        public string cd_id { get; set; }
        [DataMember]
        public string cd_seqno { get; set; }
        [DataMember]
        public string cd_rs_id { get; set; }
        
         
    }
}
