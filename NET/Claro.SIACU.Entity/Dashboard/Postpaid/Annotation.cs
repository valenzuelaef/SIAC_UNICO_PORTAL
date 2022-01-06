using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "AnnotationPostPaid")]
    public class Annotation
    {
        [Data.DbColumn("CODIGO_TICKLER")]
        [DataMember]
        public string CODIGO { get; set; }

        [Data.DbColumn("CUENTA")]
        [DataMember]
        public string CODIGO_CLIENTE { get; set; }

        [Data.DbColumn("ESTADO_ANOTACION")]
        [DataMember]
        public string ESTADO { get; set; }

        [Data.DbColumn("PRIORIDAD")]
        [DataMember]
        public string PRIORIDAD { get; set; }

        [Data.DbColumn("DESCRIPCION_CORTA")]
        [DataMember]
        public string DESCRIPCION { get; set; }

        [Data.DbColumn("USUARIO_SEGUIMIENTO")]
        [DataMember]
        public string USUARIO_SEGUIMIENTO { get; set; }

        [Data.DbColumn("FECHA_SEGUIMIENTO")]
        [DataMember]
        public string FECHA_SEGUIMIENTO { get; set; }


        [DataMember]
        public string FECHA { get; set; }


        [DataMember]
        public string SFECHA { get; set; }

        [Data.DbColumn("ACCION_SEGUIMIENTO")]
        [DataMember]
        public string ACCION_SEGUIMIENTO { get; set; }

        [Data.DbColumn("FECHA_APERTURA")]
        [DataMember]
        public string FECHA_APERTURA { get; set; }

        [Data.DbColumn("FECHA_CIERRE")]
        [DataMember]
        public string FECHA_CIERRE { get; set; }

        [Data.DbColumn("NRO_TICKLER")]
        [DataMember]
        public string NRO_TICKLER { get; set; }


        [DataMember]
        public string TIPO { get; set; }


        [DataMember]
        public string DESC_TIPO { get; set; }

        [Data.DbColumn("ESTADO")]
        [DataMember]
        public string ESTADO_ACTION { get; set; }
        [DataMember]
        public string FECHA_MODI { get; set; }
    }
}