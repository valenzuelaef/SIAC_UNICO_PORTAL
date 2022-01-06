using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "OtherChargesDetailsPostPaid")]
    public class OtherDetails
    {

        [Data.DbColumn("DESCRIPTION")]
        [DataMember]
        public string description { get; set; }


        [Data.DbColumn("AMOUNT")]
        [DataMember]
        public string amount { get; set; }


        [Data.DbColumn("REASON")]
        [DataMember]
        public string reason { get; set; }
    }
}