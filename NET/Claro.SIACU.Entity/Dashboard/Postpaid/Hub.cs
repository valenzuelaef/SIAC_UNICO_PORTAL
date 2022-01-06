using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "HubPostPaid")]
    [Data.DbTable("TEMPO")]
    public class Hub
    {
        [DataMember]
        public string HUB { get; set; }

        [DataMember]
        [Data.DbColumn("HUB_DESC")]
        public string HUB_DESC { get; set; }

        [DataMember]
        public string CMTS { get; set; }

        [DataMember]
        public string CINTILLO { get; set; }

        [DataMember]
        public string CODSUC { get; set; }

        [DataMember]
        public string NOMSUC { get; set; }

        [DataMember]
        public string DIRSUC { get; set; }

        [DataMember]
        public string NUMVIA { get; set; }

        [DataMember]
        public string NOMURB { get; set; }

        [DataMember]
        public string REFERENCIA { get; set; }

        [DataMember]
        public string IDPLANO { get; set; }
    }
}