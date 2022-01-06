using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ScheduledTransactionPostPaid")]
    public class ScheduledTransaction
    {
         [Data.DbColumn("CO_ID")]
        [DataMember]
        public string CO_ID { get; set; }
        [Data.DbColumn("CUSTOMER_ID")]
        [DataMember]
        public string CUSTOMER_ID { get; set; }
        [Data.DbColumn("SERVD_FECHAPROG")]
        [DataMember]
        public string SERVD_FECHAPROG { get; set; }
        [Data.DbColumn("SERVD_FECHA_REG")]
        [DataMember]
        public string SERVD_FECHA_REG { get; set; }
        [Data.DbColumn("SERVD_FECHA_EJEC")]
        [DataMember]
        public string SERVD_FECHA_EJEC { get; set; }
        [Data.DbColumn("SERVC_ESTADO")]
        [DataMember]
        public string SERVC_ESTADO { get; set; }
        [Data.DbColumn("DESC_ESTADO")]
        [DataMember]
        public string DESC_ESTADO { get; set; }
        [Data.DbColumn("SERVC_ESBATCH")]
        [DataMember]
        public string SERVC_ESBATCH { get; set; }
        [Data.DbColumn("SERVV_MEN_ERROR")]
        [DataMember]
        public string SERVV_MEN_ERROR { get; set; }
        [Data.DbColumn("SERVV_COD_ERROR")]
        [DataMember]
        public string SERVV_COD_ERROR { get; set; }
        [Data.DbColumn("SERVV_USUARIO_SISTEMA")]
        [DataMember]
        public string SERVV_USUARIO_SISTEMA { get; set; }
        [Data.DbColumn("SERVV_ID_EAI_SW")]
        [DataMember]
        public string SERVV_ID_EAI_SW { get; set; }
        [Data.DbColumn("SERVI_COD")]
        [DataMember]
        public string SERVI_COD { get; set; }
        [Data.DbColumn("DESC_SERVI")]
        [DataMember]
        public string DESC_SERVI { get; set; }
        [Data.DbColumn("SERVV_MSISDN")]
        [DataMember]
        public string SERVV_MSISDN { get; set; }
        [Data.DbColumn("SERVV_ID_BATCH")]
        [DataMember]
        public string SERVV_ID_BATCH { get; set; }
        [Data.DbColumn("SERVV_USUARIO_APLICACION")]
        [DataMember]
        public string SERVV_USUARIO_APLICACION { get; set; }
        [Data.DbColumn("SERVV_EMAIL_USUARIO_APP")]
        [DataMember]
        public string SERVV_EMAIL_USUARIO_APP { get; set; }
        [Data.DbColumn("SERVV_XMLENTRADA")]
        [DataMember]
        public string SERVV_XMLENTRADA { get; set; }
        [Data.DbColumn("SERVC_NROCUENTA")]
        [DataMember]
        public string SERVC_NROCUENTA { get; set; }
        [Data.DbColumn("SERVC_CODIGO_INTERACCION")]
        [DataMember]
        public string SERVC_CODIGO_INTERACCION { get; set; }
        [Data.DbColumn("SERVC_PUNTOVENTA")]
        [DataMember]
        public string SERVC_PUNTOVENTA { get; set; }
        [Data.DbColumn("SERVC_TIPO_SERV")]
        [DataMember]
        public string SERVC_TIPO_SERV { get; set; }
        [Data.DbColumn("SERVC_CO_SER")]
        [DataMember]
        public string SERVC_CO_SER { get; set; }
        [Data.DbColumn("SERVC_TIPO_REG")]
        [DataMember]
        public string SERVC_TIPO_REG { get; set; }
        [Data.DbColumn("SERVC_DES_CO_SER")]
        [DataMember]
        public string SERVC_DES_CO_SER { get; set; }
    }
}
