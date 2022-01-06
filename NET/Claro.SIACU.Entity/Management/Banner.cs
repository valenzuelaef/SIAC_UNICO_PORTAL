using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management
{
    [DataContract(Name = "BannerManagement")]
    public class Banner
    {
        [Data.DbColumn("ID_BANNER")]
        [DataMember]
        public int ID_BANNER { get; set; }
        [Data.DbColumn("MENSAJE")]
        [DataMember]
        public string MENSAJE { get; set; }
        [Data.DbColumn("ORDEN_PRIORIDAD")]
        [DataMember]
        public int ORDEN_PRIORIDAD { get; set; }
        [Data.DbColumn("FECHA_VIGENCIA_INICIO")]
        [DataMember]
        public DateTime FECHA_VIGENCIA_INICIO { get; set; }
        [Data.DbColumn("FECHA_VIGENCIA_FIN")]
        [DataMember]
        public DateTime FECHA_VIGENCIA_FIN { get; set; }
        [Data.DbColumn("ESTADO")]
        [DataMember]
        public string ESTADO { get; set; }
        [DataMember]
        public string LOGIN_REGISTRO { get; set; }
        [DataMember]
        public string LOGIN_MODIFICACION { get; set; }
        [DataMember]
        public string LOGIN { get; set; }

    }
}