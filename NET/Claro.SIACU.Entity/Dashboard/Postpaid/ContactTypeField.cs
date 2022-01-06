using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ContactFieldPostPaid")]
    public class ContactTypeField
    {
        [DataMember]
        [Data.DbColumn("TCCCN_CODIGO")]
        public int TCCCN_CODIGO { get; set; }
        [DataMember]
        [Data.DbColumn("TCCCN_TIPODATO")]
        public int TCCCN_TIPODATO { get; set; }
        [DataMember]
        [Data.DbColumn("TCCCN_TIPODATOOPC")]
        public int TCCCN_TIPODATOOPC { get; set; }
        [DataMember]
        [Data.DbColumn("TCCCN_LONGITUD")]
        public decimal  TCCCN_LONGITUD { get; set; }
        [DataMember]
        [Data.DbColumn("TCCCV_NOMBRE")]
        public string TCCCV_NOMBRE { get; set; }
        [DataMember]
        [Data.DbColumn("SPERV_NOMBRE")]
        public string SPERV_NOMBRE { get; set; }
        [DataMember]
        [Data.DbColumn("SPERV_TIPODATO")]
        public string SPERV_TIPODATO { get; set; }
        [DataMember]
        [Data.DbColumn("TCCCC_EDITABLE")]
        public string TCCCC_EDITABLE { get; set; }
        [DataMember]
        [Data.DbColumn("TCCCN_ORDEN")]
        public int TCCCN_ORDEN { get; set; }
    }
}
