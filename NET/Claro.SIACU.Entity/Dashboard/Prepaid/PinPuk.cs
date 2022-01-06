using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "PinkpukPrePaid")]
    public class PinPuk
    {
        [Data.DbColumn("ICC_ID")]
        [DataMember]
        public string ICC_ID { get; set; }

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

        [Data.DbColumn("PIN2")]
        [DataMember]
        public string PIN2 { get; set; }

        [Data.DbColumn("PUK2")]
        [DataMember]
        public string PUK2 { get; set; }
    }
}
