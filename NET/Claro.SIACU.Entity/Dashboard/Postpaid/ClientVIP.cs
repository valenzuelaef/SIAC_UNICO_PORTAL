using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    public class ClientVIP
    {
        [Data.DbColumn("MSISDN")]
        [DataMember]
        public string MSISDN { get; set; }

        [Data.DbColumn("ESTADO")]
        [DataMember]
        public string ESTADO { get; set; }

        [Data.DbColumn("FECHA_REGISTRO")]
        [DataMember]
        public DateTime FECHA_REGISTRO { get; set; }

        [Data.DbColumn("FECHA_BAJA")]
        [DataMember]
        public DateTime FECHA_BAJA { get; set; }

        [Data.DbColumn("NOTAS")]
        [DataMember]
        public string NOTAS { get; set; }
    }
}
