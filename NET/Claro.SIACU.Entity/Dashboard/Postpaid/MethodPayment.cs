using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "MethodPaymentPostPaid")]
    public class MethodPayment
    {
        [Data.DbColumn("NUMERO_SOLICITUD")]
        [DataMember]
        public string NUMERO_SOLICITUD { get; set; }

        [Data.DbColumn("FECHA_REGISTRO")]
        [DataMember]
        public string FECHA_REGISTRO { get; set; }

        [Data.DbColumn("TIPO_DOMICILIACION")]
        [DataMember]
        public string TIPO_DOMICILIACION { get; set; } 

        [Data.DbColumn("TIPO_DEBITO")]
        [DataMember]
        public string TIPO_DEBITO { get; set; } 

        [Data.DbColumn("ESTADO_SOLICITUD")]
        [DataMember]
        public string ESTADO_SOLICITUD { get; set; }

        [Data.DbColumn("MONTO_MAXIMO")]
        [DataMember]
        public string MONTO_MAXIMO { get; set; }

        [Data.DbColumn("DESCRIPCION_LARGA")]
        [DataMember]
        public string DESCRIPCION_LARGA { get; set; } 

        [Data.DbColumn("TIPO_CUENTA_BANCARIA")]
        [DataMember]
        public string TIPO_CUENTA_BANCARIA { get; set; } 

        [Data.DbColumn("MONEDA")]
        [DataMember]
        public string MONEDA { get; set; } 

        [Data.DbColumn("NUMERO_CUENTA_TARJETA")]
        [DataMember]
        public string NUMERO_CUENTA_TARJETA { get; set; }

        [Data.DbColumn("FECHA_EXPIRACION_TARJETA")]
        [DataMember]
        public string FECHA_EXPIRACION_TARJETA { get; set; }

        [Data.DbColumn("FECHA_APROBADO")]
        [DataMember]
        public string FECHA_APROBADO { get; set; }

        [Data.DbColumn("FECHA_RECHAZO")]
        [DataMember]
        public string FECHA_RECHAZO { get; set; }

        [Data.DbColumn("MOTIVO_SOLICITUD")]
        [DataMember]
        public string MOTIVO_SOLICITUD { get; set; }

        [Data.DbColumn("MOTIVO")]
        [DataMember]
        public string MOTIVO { get; set; } 

        [Data.DbColumn("FECHA_DESAFILIACION")]
        [DataMember]
        public string FECHA_DESAFILIACION { get; set; }

        [Data.DbColumn("COD_ENTIDAD")]
        [DataMember]
        public string COD_ENTIDAD { get; set; } 

        [Data.DbColumn("DESC_MONEDA")]
        [DataMember]
        public string DESC_MONEDA { get; set; } 

        [Data.DbColumn("DESC_TIPO_CUENTA")]
        [DataMember]
        public string DESC_TIPO_CUENTA { get; set; } 

        [Data.DbColumn("DNI_TITULAR")]
        [DataMember]
        public string DNI_TITULAR { get; set; }

        [Data.DbColumn("NOMBRE_TITULAR")]
        [DataMember]
        public string NOMBRE_TITULAR { get; set; }

        [Data.DbColumn("FECHA_PROCESO")]
        [DataMember]
        public string FECHA_PROCESO { get; set; }

        [Data.DbColumn("FECHA_SOLICITUD")]
        [DataMember]
        public string FECHA_SOLICITUD { get; set; }

        
        [DataMember]
        public string DES_ESTADO_SOLICITUD { get; set; }
    }
}