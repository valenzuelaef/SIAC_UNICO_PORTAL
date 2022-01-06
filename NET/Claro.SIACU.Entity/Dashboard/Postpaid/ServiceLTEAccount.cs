using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ServiceLTEAccountPostPaid")]
    public class ServiceLTEAccount
    {
        [Data.DbColumn("MSISDN")]
        [DataMember]
        public string MSISDN { get; set; }

        [Data.DbColumn("CO_ID")]
        [DataMember]
        public string CO_ID { get; set; }

        [Data.DbColumn("FECHA_ACTIVACION")]
        [DataMember]
        public string FECHA_ACTIVACION { get; set; }

        [Data.DbColumn("ESTADO")]
        [DataMember]
        public string ESTADO { get; set; }

        [Data.DbColumn("FECHA_ESTADO")]
        [DataMember]
        public string FECHA_ESTADO { get; set; }

        [Data.DbColumn("MOTIVO_ESTADO")]
        [DataMember]
        public string MOTIVO_ESTADO { get; set; }

        [Data.DbColumn("COD_PLAN")]
        [DataMember]
        public string COD_PLAN { get; set; }

        [Data.DbColumn("PLAN_TARIFARIO")]
        [DataMember]
        public string PLAN_TARIFARIO { get; set; }

        [Data.DbColumn("PLAZO_ACUERDO")]
        [DataMember]
        public string PLAZO_ACUERDO { get; set; }

        [Data.DbColumn("PTO_VENTA")]
        [DataMember]
        public string PTO_VENTA { get; set; }

        [Data.DbColumn("VENDEDOR")]
        [DataMember]
        public string VENDEDOR { get; set; }

        [Data.DbColumn("CAMPANA")]
        [DataMember]
        public string CAMPANA { get; set; }

        [Data.DbColumn("INTERNET")]
        [DataMember]
        public string INTERNET { get; set; }

        [Data.DbColumn("TELEFONIA")]
        [DataMember]
        public string TELEFONIA { get; set; }

        [Data.DbColumn("CABLE_TV")]
        [DataMember]
        public string CABLE_TV { get; set; }

        [Data.DbColumn("TIPO_PROD")]
        [DataMember]
        public string TIPO_PROD { get; set; }

        [Data.DbColumn("TIPO_PLAN")]
        [DataMember]
        public string TIPO_PLAN { get; set; }
       

    }
}
