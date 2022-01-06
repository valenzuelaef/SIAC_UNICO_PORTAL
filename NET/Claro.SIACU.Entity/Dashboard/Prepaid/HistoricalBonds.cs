using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "HistoricalBondsPrepaid")]
    public class HistoricalBonds
    {
        [Data.DbColumn("ID_TRANSACCION")]
        [DataMember]
        public string ID_TRANSACCION { get; set; }

        [Data.DbColumn("TRANSACCION")]
        [DataMember]
        public string TRANSACCION { get; set; }

        [Data.DbColumn("FECHA")]
        [DataMember]
        public string FECHA { get; set; }

        [Data.DbColumn("APLICATIVO")]
        [DataMember]
        public string APLICATIVO { get; set; }

        [Data.DbColumn("PROMOCION")]
        [DataMember]
        public string PROMOCION { get; set; }
    }
}
