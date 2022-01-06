using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ContactTypePostPaid")]
    public class ContactType
    {
        [DataMember]
        [Data.DbColumn("TCCTN_CODIGO")]
        public int TCCTN_CODIGO { get; set; }
        [DataMember]
        public int TCCTN_ORDEN { get; set; }
        [DataMember]
        [Data.DbColumn("TCCTV_NOMBRE")]
        public string TCCTV_NOMBRE { get; set; }
        [DataMember]
        public string  TCCTV_SISACT_SIAC_DES { get; set; }
        [DataMember]
        public string  TCCTV_OBLIGATORIO_DES { get; set; }
        [DataMember]
        public string  TCCT_ESTADO_DES { get; set; }
        [DataMember]
        [Data.DbColumn("TCCTC_COPIABLE")]
        public bool TCCTC_COPIABLE { get; set; }
        [DataMember]
        [Data.DbColumn("TCCTC_OBLIGATORIO")]
        public bool TCCTC_OBLIGATORIO { get; set; }
        [DataMember]
        public bool TCCTC_ESTADO { get; set; }
        [DataMember]
        [Data.DbColumn("TCCTI_MINREGISTROS")]
        public int TCCTI_MINREGISTROS { get; set; }
        [DataMember]
        [Data.DbColumn("TCCTI_MAXREGISTROS")]
        public int TCCTI_MAXREGISTROS { get; set; }
        [DataMember]
        [Data.DbColumn("TCCTC_VISIBLESIACPOST")]
        public bool TCCTC_VISIBLESIACPOST { get; set; }
        [DataMember]
        [Data.DbColumn("TCCTC_VISIBLESISACT")]
        public bool TCCTC_VISIBLESISACT { get; set; }

    }
}
