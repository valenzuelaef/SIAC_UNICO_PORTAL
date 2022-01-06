using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard
{
    [DataContract(Name = "Instant")]
    public class Instant
    {

        [Data.DbColumn("ID_INSTANTANEA")]
        [DataMember]
        public int ID_INSTANTANEA { get; set; }

        [DataMember]
        public string DATO { get; set; }

        [DataMember]
        public string CONTRATO { get; set; }

        [DataMember]
        public string CUENTA { get; set; }

        [DataMember]
        public string TIPOTELEFONO { get; set; }

        [Data.DbColumn("DESCRIPCION")]
        [DataMember]
        public string DESCRIPCION { get; set; }

        [Data.DbColumn("FECHA_VIGENCIA_INI")]
        [DataMember]
        public DateTime FECHA_VIGENCIA_INICIO { get; set; }

        [Data.DbColumn("FECHA_VIGENCIA_FIN")]
        [DataMember]
        public DateTime FECHA_VIGENCIA_FIN { get; set; }

        //  [Data.DbColumn("VIGENCIA")]
        [DataMember]
        public string VIGENCIA { get; set; }

        [DataMember]
        public string LOGIN { get; set; }

        [DataMember]
        public string NOMBRE_ARCHIVO_INSTANTANEA { get; set; }

        [DataMember]
        public string NOMBRE_ARCHIVO_DESCRIPCION { get; set; }

        [DataMember]
        public DateTime FECHA_REGISTRO { get; set; }

        [DataMember]
        public int NRO { get; set; }

        [DataMember]
        public string ESTADO { get; set; }

        [Data.DbColumn("COLOR")]
        [DataMember]
        public string COLOR { get; set; }

        [DataMember]
        public string LOGIN_REGISTRO { get; set; }

        [DataMember]
        public string LOGIN_MODIFICACION { get; set; }
    }
}