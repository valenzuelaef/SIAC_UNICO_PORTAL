using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "PinyPukPostPaid")]
    public class PinPuk
    {
        [Data.DbColumn("ICC_ID")]
        [DataMember]
        public string ICC_IC { get; set; }

        [Data.DbColumn("IMSI")]
        [DataMember]
        public string IMSI { get; set; }

        [Data.DbColumn("MSISDN")]
        [DataMember]
        public string MSISDN { get; set; }

        [Data.DbColumn("PIN1")]
        [DataMember]
        public string PIN1 { get; set; }

        [Data.DbColumn("PUK1")]
        [DataMember]
        public string PUK1 { get; set; }

        [Data.DbColumn("CUENTA")]
        [DataMember]
        public string CUENTA { get; set; }

        [Data.DbColumn("RAZON_SOCIAL")]
        [DataMember]
        public string RAZON_SOCIAL { get; set; }
    }
}