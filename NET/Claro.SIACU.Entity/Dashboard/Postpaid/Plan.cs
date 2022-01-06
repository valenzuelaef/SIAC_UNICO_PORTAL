using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "PlanPostPaid")]
    public class Plan
    {
        
        [DataMember]
        public int IDENTIFICADOR_IT { get; set; }

       
        [DataMember]
        public string CLIENTE_ID { get; set; }

        [Data.DbColumn("TELEFONO")]
        [DataMember]
        public string TELEFONO { get; set; }

        [Data.DbColumn("SIM_CARD")]
        [DataMember]
        public string SIMCARD { get; set; }

        [Data.DbColumn("NICHO_ID")]
        [DataMember]
        public string NICHOID { get; set; }

        [Data.DbColumn("PLAN")]
        [DataMember]
        public string DESCRIPCION_PLAN { get; set; }

        [Data.DbColumn("ACCESO")]
        [DataMember]
        public string CARGO_FIJO { get; set; }

        [Data.DbColumn("FECHA_ACTIV")]
        [DataMember]
        public string FECHA_ACTIVACION { get; set; }

        [Data.DbColumn("LDI")]
        [DataMember]
        public string LDI { get; set; }

        [Data.DbColumn("ROAM_INT")]
        [DataMember]
        public string ROAMING_INTERNACIONAL { get; set; }

        [Data.DbColumn("NIV_DE_HAB")]
        [DataMember]
        public string NIVEL_HABILITACION { get; set; }

        [Data.DbColumn("RPV")]
        [DataMember]
        public string TARIFA_PREFERENCIALRPC { get; set; }

        [Data.DbColumn("F_COSTO_RPV_NAC")]
        [DataMember]
        public string COSTORPC_ILIMITADO { get; set; }

        [Data.DbColumn("COSTO_RPV")]
        [DataMember]
        public string COSTORPC { get; set; }

        [Data.DbColumn("CTA_PERS_RECARGA")]
        [DataMember]
        public string CUENTAPERSONAL_RECARGA { get; set; }

        [Data.DbColumn("SMS")]
        [DataMember]
        public string ENVIO_SMS { get; set; }

        [Data.DbColumn("MMS")]
        [DataMember]
        public string GPRS_MMS { get; set; }

        [Data.DbColumn("BLACKBERRY")]
        [DataMember]
        public string BLACKBERRY { get; set; }

        [Data.DbColumn("TOPE_SOLES_ADIC")]
        [DataMember]
        public string TOPE_SOLESADICIONALES { get; set; }

        [Data.DbColumn("TOPE_BOLSA_EXACTO")]
        [DataMember]
        public string TOPE_BOLSAEXACTO { get; set; }

        [Data.DbColumn("CLARO_DIRECTO")]
        [DataMember]
        public string CLARO_DIRECTO { get; set; }

        [Data.DbColumn("CAMPANA")]
        [DataMember]
        public string CONSIN_EQUIPO { get; set; }

        [Data.DbColumn("CONSULTOR")]
        [DataMember]
        public string CONSULTOR_BUSINESS { get; set; }

        [Data.DbColumn("CODIGO_WF")]
        [DataMember]
        public string CODIGO_WF { get; set; }

        [Data.DbColumn("ESTADO")]
        [DataMember]
        public string ESTADO { get; set; }

        [Data.DbColumn("MOTIVO")]
        [DataMember]
        public string MOTIVO { get; set; }

        [Data.DbColumn("CONSULTOR_RENOVACION")]
        [DataMember]
        public string CONSULTOR_RENOVACION { get; set; }

        [Data.DbColumn("FECHA_EQUIPO_RENOV")]
        [DataMember]
        public string FECHAENTREGA_EQUIPORENOVACION { get; set; }

        [Data.DbColumn("FECHA_ACTIV_RENOV")]
        [DataMember]
        public string FECHAACTVACION_RENOVACION { get; set; }

        [Data.DbColumn("TIM_DATA")]
        [DataMember]
        public string TIM_DATA { get; set; }

        [Data.DbColumn("TIM_FAX")]
        [DataMember]
        public string TIM_FAX { get; set; }

        [Data.DbColumn("PAQUETE_SMS")]
        [DataMember]
        public string PAQUETE_SMS { get; set; }

        [Data.DbColumn("PAQUETE_DATA")]
        [DataMember]
        public string PAQUETE_DATA { get; set; }

        [Data.DbColumn("SEGURO")]
        [DataMember]
        public string SEGURO { get; set; }

        [Data.DbColumn("PRESTAMO")]
        [DataMember]
        public string PRESTAMO { get; set; }

        [Data.DbColumn("TIM_PRO_CONNEXION")]
        [DataMember]
        public string TIM_PROCONEXION { get; set; }

        [Data.DbColumn("LBS")]
        [DataMember]
        public string LBS { get; set; }

        [Data.DbColumn("SOLSMS")]
        [DataMember]
        public string SOLSMS { get; set; }

        [Data.DbColumn("RTP")]
        [DataMember]
        public string RTP { get; set; }

        [Data.DbColumn("HABILITACION")]
        [DataMember]
        public string HABILITACION { get; set; }

        [Data.DbColumn("F_LDI")]
        [DataMember]
        public string F_LDI { get; set; }

        [Data.DbColumn("F_ROAM_INT")]
        [DataMember]
        public string F_ROAMING_INTERNACIONAL { get; set; }

        [Data.DbColumn("F_TIM_DATA")]
        [DataMember]
        public string F_TIM_DATA { get; set; }

        [Data.DbColumn("F_TIM_FAX")]
        [DataMember]
        public string F_TIM_FAX { get; set; }

        [Data.DbColumn("F_PAQUETE_SMS")]
        [DataMember]
        public string F_PAQUETE_SMS { get; set; }

        [Data.DbColumn("F_PAQUETE_DATA")]
        [DataMember]
        public string F_PAQUETE_DATA { get; set; }

        [Data.DbColumn("F_NIV_DE_HAB")]
        [DataMember]
        public string F_NIVEL_HABILITACION { get; set; }

        [Data.DbColumn("F_RPV")]
        [DataMember]
        public string F_RPV { get; set; }

        [Data.DbColumn("F_COSTO_RPV")]
        [DataMember]
        public string F_COSTORPV { get; set; }

        [Data.DbColumn("F_COSTO_RPV_NAC")]
        [DataMember]
        public string F_COSTORPC_ILIMITADO { get; set; }

        [Data.DbColumn("F_SEGURO")]
        [DataMember]
        public string F_SEGURO { get; set; }

        [Data.DbColumn("F_PRESTAMO")]
        [DataMember]
        public string F_PRESTAMO { get; set; }

        [Data.DbColumn("F_TIM_PRO_CONNEXION")]
        [DataMember]
        public string F_TIM_PROCONEXION { get; set; }

        [Data.DbColumn("GPRS")]
        [DataMember]
        public string GPRS { get; set; }
        [Data.DbColumn("F_GPRS")]
        [DataMember]
        public string F_GPRS { get; set; }
        [Data.DbColumn("F_LBS")]
        [DataMember]
        public string F_LBS { get; set; }
        [Data.DbColumn("F_SOLSMS")]
        [DataMember]
        public string F_SOLSMS { get; set; }

        [Data.DbColumn("F_RTP")]
        [DataMember]
        public string F_RTP { get; set; }

        [Data.DbColumn("F_HABILITACION")]
        [DataMember]
        public string F_HABILITACION { get; set; }

        [Data.DbColumn("F_CAMPANA")]
        [DataMember]
        public string F_CONSIN_EQUIPO { get; set; }

        [Data.DbColumn("F_EJECUTIVO")]
        [DataMember]
        public string F_EJECUTIVO { get; set; }

        [Data.DbColumn("BOLSA_SERVICIOS")]
        [DataMember]
        public string BOLSA_SERVICIO { get; set; }

        [Data.DbColumn("F_SMS_MAIL")]
        [DataMember]
        public string F_SMS_MAIL { get; set; }

        [Data.DbColumn("SMS_MAIL")]
        [DataMember]
        public string SMS_MAIL { get; set; }

        [Data.DbColumn("F_RPCE_ILIMITADO")]
        [DataMember]
        public string F_RPC_ILIMITADO { get; set; }

        [Data.DbColumn("RPCE_ILIMITADO")]
        [DataMember]
        public string RPC_ILIMITADO { get; set; }

        [Data.DbColumn("F_FACT_DETALLADA")]
        [DataMember]
        public string F_FACTDET { get; set; }

        [Data.DbColumn("FACT_DETALLADA")]
        [DataMember]
        public string FACTDET { get; set; }

        [Data.DbColumn("F_FACT_DET_MOD_A")]
        [DataMember]
        public string F_FACTDET_MODA { get; set; }

        [Data.DbColumn("FACT_DET_MOD_A")]
        [DataMember]
        public string FACTDET_MODA { get; set; }

        [Data.DbColumn("F_CLARO_DIRECTO")]
        [DataMember]
        public string F_CLARO_DIRECTO { get; set; }

        [Data.DbColumn("F_CARGO_FACT_DET")]
        [DataMember]
        public string F_CARGO_FACTDET { get; set; }

        [Data.DbColumn("CARGO_FACT_DET")]
        [DataMember]
        public string CARGO_FACTDET { get; set; }

        [Data.DbColumn("F_CLARO_FAX")]
        [DataMember]
        public string F_CLARO_FAX { get; set; }

        [Data.DbColumn("CLARO_FAX")]
        [DataMember]
        public string CLARO_FAX { get; set; }

        [Data.DbColumn("F_CLARO_DATA")]
        [DataMember]
        public string F_CLARO_DATA { get; set; }

        [Data.DbColumn("CLARO_DATA")]
        [DataMember]
        public string CLARO_DATA { get; set; }

        [Data.DbColumn("F_CONTROL_CONSUMO_ADIC")]
        [DataMember]
        public string F_CONTROL_CONSUMOADIC { get; set; }

        [Data.DbColumn("F_CTA_PERS_RECARGA")]
        [DataMember]
        public string F_CTAPERSONAL_RECARGA { get; set; }

        [Data.DbColumn("F_SMS")]
        [DataMember]
        public string F_SMS { get; set; }

        [Data.DbColumn("F_MMS")]
        [DataMember]
        public string F_MMS { get; set; }

        [Data.DbColumn("F_BLACKBERRY")]
        [DataMember]
        public string F_BLACKBERRY { get; set; }

        [Data.DbColumn("F_REPOSICION")]
        [DataMember]
        public string F_REPOSICION { get; set; }

        [Data.DbColumn("REPOSICION")]
        [DataMember]
        public string REPOSICION { get; set; }

        [Data.DbColumn("F_CLAROBANCA")]
        [DataMember]
        public string F_CLARO_BANCA { get; set; }

        [Data.DbColumn("CLAROBANCA")]
        [DataMember]
        public string CLARO_BANCA { get; set; }
    }
}