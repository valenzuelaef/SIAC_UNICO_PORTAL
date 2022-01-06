using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "CallDetailTimServicePostPaid")]
    public class CallDetailTimService
    {
        [Data.DbColumn("MSISDN")]
        [DataMember]
        public string MSISDN { get; set; }


        [Data.DbColumn("AMOUNT")]
        [DataMember]
        public decimal AMOUNT { get; set; }


        [Data.DbColumn("PERIODO")]
        [DataMember]
        public string PERIODO { get; set; }


        [Data.DbColumn("RATEPLAN")]
        [DataMember]
        public string RATEPLAN { get; set; }
    }
}
