using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard
{
    [DataContract(Name = "Banner")]
    [Data.DbTable("TEMPO")]
    public class Banner
    {

        public int ID_BANNER { get; set; }
        [Data.DbColumn("MENSAJE")]
        public string MENSAJE { get; set; }
        [Data.DbColumn("ORDEN_PRIORIDAD")]
        public int ORDEN_PRIORIDAD { get; set; }
        [Data.DbColumn("FECHA_VIGENCIA_INICIO")]
        public DateTime FECHA_VIGENCIA_INICIO { get; set; }
        [Data.DbColumn("FECHA_VIGENCIA_FIN")]
        public DateTime FECHA_VIGENCIA_FIN { get; set; }
        [Data.DbColumn("ESTADO")]
        public string ESTADO { get; set; }
    }
}