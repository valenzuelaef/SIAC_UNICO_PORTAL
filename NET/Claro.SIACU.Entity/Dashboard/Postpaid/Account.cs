using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "AccountPostPaid")]
    public class Account
    {
        [DataMember]
        public string TIPO_CLIENTE { get; set; }

        [DataMember]
        public string MODALIDAD { get; set; }

        [DataMember]
        public string SEGMENTO { get; set; }

        [DataMember]
        public string CICLO_FACTURACION { get; set; }

        [DataMember]
        public string NICHO { get; set; }
        [DataMember]
        public string plataformaAT { get; set; }

        [DataMember]
        [Data.DbColumn("ESTADO")]
        public string ESTADO_CUENTA { get; set; }

        [DataMember]
        public string RESPONSABLE_PAGO { get; set; }

        [DataMember]
        public string LIMITE_CREDITO { get; set; }

        [DataMember]
        public string TOTAL_LINEAS { get; set; }

        [DataMember]
        public string TOTAL_CUENTAS { get; set; }

        [DataMember]
        public string FECHA_ACT { get; set; }

        [DataMember]
        public string CONSULTOR { get; set; }

        [DataMember]
        public string CONSULTOR_ACCOUNT { get; set; }


        [DataMember]
        [Data.DbColumn("NOMBRE")]
        public string NOMBRE { get; set; }

        [DataMember]
        public string SALDO { get; set; }

        [DataMember]
        public string FECHA_EXPIRACION { get; set; }


        [DataMember]
        [Data.DbColumn("APELLIDOS")]
        public string APELLIDOS { get; set; }


        [DataMember]
        [Data.DbColumn("CUENTA_PADRE")]
        public string CUENTA_PADRE { get; set; }


        [DataMember]
        [Data.DbColumn("CUENTA")]
        public string CUENTAID { get; set; }


        [DataMember]
        [Data.DbColumn("NIVEL")]
        public string NIVEL { get; set; }


        [DataMember]
        [Data.DbColumn("CUSTOMER_ID")]
        public string CUSTOMERID { get; set; }
        [DataMember]
        public string billingAccountId { get; set; }
        [DataMember]
        public string bmIdPub { get; set; }
        [DataMember]
        public string contactSeqno { get; set; }

        [DataMember]
        public string IsSendEmail { get; set; }

        [DataMember]
        public string SALDO_LIMITE_CREDITO { get; set; }


    }
}