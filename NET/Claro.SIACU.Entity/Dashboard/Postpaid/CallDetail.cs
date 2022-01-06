using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "CallDetailPostPaid")]
    public class CallDetail
    {

        [Data.DbColumn("MSISDN")]
        [DataMember]
        public string MSISDN { get; set; }

        [DataMember]
        public string CALLANTES { get; set; }

        [DataMember]
        public string CALLDESPUES { get; set; }

        [DataMember]
        public string CALLDESTINATION { get; set; }


        [Data.DbColumn("CALLNUMBER")]
        [DataMember]
        public string CALLNUMBER { get; set; }


        [Data.DbColumn("CALLDATE")]
        [DataMember]
        public string CALLDATE { get; set; }


        [Data.DbColumn("CALLTIME")]
        [DataMember]
        public string CALLTIME { get; set; }


        [Data.DbColumn("CALLDURATION")]
        [DataMember]
        public string CALLDURATION { get; set; }


        [Data.DbColumn("CALLTOTAL")]
        [DataMember]
        public decimal CALLTOTAL { get; set; }


        [Data.DbColumn("CALLORIGIN")]
        [DataMember]
        public string CALLORIGIN { get; set; }


        [Data.DbColumn("TIPOLLAMADA")]
        [DataMember]
        public string TIPOLLAMADA { get; set; }
    }
}