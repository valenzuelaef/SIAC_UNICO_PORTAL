using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    public class LocalTrafficDetailCallTP
    {
        [Data.DbColumn("CALLDURATION")]
        [DataMember]
        public string CALLDURATION { get; set; }


        [Data.DbColumn("CALLTIME")]
        [DataMember]
        public string CALLTIME { get; set; }

        [Data.DbColumn("CALLTIMEFIN")]
        [DataMember]
        public string CALLTIMEFIN { get; set; }

        [Data.DbColumn("CALLDATEFIN")]
        [DataMember]
        public string CALLDATEFIN { get; set; }

        [Data.DbColumn("CALLNUMBER")]
        [DataMember]
        public string CALLNUMBER { get; set; }


        [Data.DbColumn("CALLDATE")]
        [DataMember]
        public string CALLDATE { get; set; }


        [Data.DbColumn("CALLDESTINATION")]
        [DataMember]
        public string CALLDESTINATION { get; set; }


        [Data.DbColumn("CALLDURATIONMIN")]
        [DataMember]
        public string CALLDURATIONMIN { get; set; }


        [Data.DbColumn("CALLTOTAL")]
        [DataMember]
        public string CALLTOTAL { get; set; }

        [Data.DbColumn("CALLAMBITO")]
        [DataMember]
        public string CALLAMBITO { get; set; }
    }
}
