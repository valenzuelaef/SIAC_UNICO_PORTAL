using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "CallDetailSMSPostPaid")]
    public class CallDetailSMS
    {

        [Data.DbColumn("SMSNUMBER")]
        [DataMember]
        public string SMSNUMBER { get; set; }


        [Data.DbColumn("SMSDATE")]
        [DataMember]
        public string SMSDATE { get; set; }


        [Data.DbColumn("SMSTIME")]
        [DataMember]
        public string SMSTIME { get; set; } 


        [Data.DbColumn("SMSTOTAL")]
        [DataMember]
        public decimal SMSTOTAL { get; set; }


        [Data.DbColumn("SMSDESTINATION")]
        [DataMember]
        public string SMSDESTINATION { get; set; }

    }
}
