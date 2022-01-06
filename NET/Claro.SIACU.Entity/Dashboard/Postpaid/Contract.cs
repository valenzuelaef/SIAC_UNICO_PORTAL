using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ContractPostPaid")]
    public class Contract
    {
        [Data.DbColumn("CODIGO")]
        [DataMember]
        public string CODIGO_CLIENTE { get; set; }

        [Data.DbColumn("TIPO_CLIENTE")]
        [DataMember]
        public string TIPO_CLIENTE { get; set; }

        [Data.DbColumn("RAZON_SOCIAL")]
        [DataMember]
        public string RAZON_SOCIAL { get; set; }

        [Data.DbColumn("PLAN_TARIFARIO")]
        [DataMember]
        public string PLAN_TARIFARIO { get; set; }

        [Data.DbColumn("TELEFONO")]
        [DataMember]
        public string TELEFONO { get; set; }

        [Data.DbColumn("SIMCARD")]
        [DataMember]
        public string SIM_CARD { get; set; }

        [Data.DbColumn("FECHA_ACTIVACION")]
        [DataMember]
        public string FECHA_ACTIVACION { get; set; }

        [Data.DbColumn("FECHA_SUSPENSION")]
        [DataMember]
        public string FECHA_SUSPENSION { get; set; }

        [Data.DbColumn("MOTIVO_SUSPENSION")]
        [DataMember]
        public string MOTIVO_SUSPENSION { get; set; }

        [Data.DbColumn("USUARIO")]
        [DataMember]
        public string USUARIO { get; set; }


        [DataMember]
        public bool ESTADO_UMBRAL { get; set; }
    }
}