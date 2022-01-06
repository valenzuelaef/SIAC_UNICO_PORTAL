using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "HistoricalTriationRFAPrepaid")]
    public class HistoricalTriationRFA
    {
        [Data.DbColumn("OPCION")]
        [DataMember]
        public string Opcion { get; set; }

        [Data.DbColumn("OPCION")]
        [DataMember]
        public string OpcionTexto { get; set; }

        [Data.DbColumn("TRANSACCION")]
        [DataMember]
        public string Transaccion { get; set; }

        [Data.DbColumn("FECHA")]
        [DataMember]
        public string Fecha { get; set; }

        [Data.DbColumn("APLICATIVO")]
        [DataMember]
        public string Aplicativo { get; set; }

        [Data.DbColumn("ID")]
        [DataMember]
        public string IdInteraccion { get; set; }
    }
}
