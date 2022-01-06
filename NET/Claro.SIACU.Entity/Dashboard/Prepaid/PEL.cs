using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "PELPrePaid")]
    public class PEL
    {
        [Data.DbColumn("MSISDN")]
        [DataMember]
        public int P_TELEFONO { get; set; }

        [Data.DbColumn("P_ICCID")]
        [DataMember]
        public string P_ICCID { get; set; }

        [Data.DbColumn("P_ICCID_COD")]
        [DataMember]
        public string P_ICCID_COD { get; set; }

        [Data.DbColumn("P_IMEI")]
        [DataMember]
        public string P_IMEI { get; set; }

        [Data.DbColumn("P_IMEI_COD")]
        [DataMember]
        public string P_IMEI_COD { get; set; }

        [Data.DbColumn("P_OFICINA")]
        [DataMember]
        public string P_OFICINA { get; set; }

        [Data.DbColumn("P_FECHA_ACT")]
        [DataMember]
        public string P_FECHA_ACT { get; set; }

        [Data.DbColumn("P_PRODEDENCIA")]
        [DataMember]
        public string P_PROCEDENCIA { get; set; }
    }
}
