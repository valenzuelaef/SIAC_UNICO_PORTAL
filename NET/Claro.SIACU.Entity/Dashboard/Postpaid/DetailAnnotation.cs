using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "DetailAnnotationPostPaid")]
    public class DetailAnnotation
    {
        [Data.DbColumn("DESCRIPCION")]
        [DataMember]
        public string DESCRIPCION { get; set; }

        [Data.DbColumn("USUARIO_APERTURA")]
        [DataMember]
        public string USUARIO_APERTURA { get; set; }

        [Data.DbColumn("USUARIO_MODIFICACION")]
        [DataMember]
        public string USUARIO_MODIFICACION { get; set; }

        [Data.DbColumn("USUARIO_CIERRE")]
        [DataMember]
        public string USUARIO_CIERRE { get; set; }

        [Data.DbColumn("FECHA_APERTURA")]
        [DataMember]
        public string FECHA_APERTURA { get; set; }

        [Data.DbColumn("FECHA_MODIFICACION")]
        [DataMember]
        public string FECHA_MODIFICACION { get; set; }

        [Data.DbColumn("FECHA_SEGUIMIENTO")]
        [DataMember]
        public string FECHA_SEGUIMIENTO { get; set; }

        [Data.DbColumn("FECHA_CIERRE")]
        [DataMember]
        public string FECHA_CIERRE { get; set; }
    }
}
