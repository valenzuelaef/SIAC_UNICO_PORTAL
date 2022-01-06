using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ConsumeLimitPostPaid")]
    [Data.DbTable("TEMPO")]
    public class ConsumeLimit
    {
        [DataMember]
        [Data.DbColumn("DESC_SERV")]
        public string DESC_SERV { get; set; }

        [DataMember]
        [Data.DbColumn("CO_SER")]
        public string CO_SER { get; set; }

        [DataMember]
        [Data.DbColumn("TOPE")]
        public string TOPE { get; set; }

        [DataMember]
        [Data.DbColumn("CICLO")]
        public string CICLO { get; set; }

        [DataMember]
        [Data.DbColumn("DES")]
        public string DES { get; set; }
    }
}