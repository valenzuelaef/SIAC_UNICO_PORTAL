using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ServiceHFCAccountPostPaid")]
    public class ServiceHFCAccount
    {

        [Data.DbColumn("CO_ID")]
        [DataMember]
        public string CONTRATO { get; set; }

        [Data.DbColumn("ESTADO")]
        [DataMember]
        public string ESTADO { get; set; }

        [Data.DbColumn("PLAN_TARIFARIO")]
        [DataMember]
        public string PLAN_TARIFARIO { get; set; }

        [Data.DbColumn("DireccionInstalacion")]
        [DataMember]
        public string DIR_INSTALACION { get; set; }

        [Data.DbColumn("COMBO")]
        [DataMember]
        public string COMBO { get; set; }

        [Data.DbColumn("DESCUENTO")]
        [DataMember]
        public string DESCUENTO { get; set; }

        [Data.DbColumn("TELEFONIA")]
        [DataMember]
        public string TELEFONIA_FIJA { get; set; }

        [Data.DbColumn("INTERNET")]
        [DataMember]
        public string INTERNET_FIJO { get; set; }

        [Data.DbColumn("CABLE_TV")]
        [DataMember]
        public string CLAROTV { get; set; }

        [DataMember]
        public string COD_PLAN { get; set; }

        [DataMember]
        public string COD_COMBO { get; set; }


    }
}
