using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "DetailLocalTrafficPostPaid")]
    public class LocalTrafficDetail
    {

        [Data.DbColumn("MSISDN")]
        [DataMember]
        public string MSISDN { get; set; }


        [Data.DbColumn("RTP")]
        [DataMember]
        public string RTP { get; set; }

        [Data.DbColumn("ONNET")]
        [DataMember]
        public string ONNET { get; set; }

        [Data.DbColumn("OFF_NET")]
        [DataMember]
        public string OFF_NET { get; set; }


        [Data.DbColumn("OFF_ONNET_OFFNET")]
        [DataMember]
        public string OFF_ONNET_OFFNET { get; set; }


        [Data.DbColumn("OFFNET_A_FIJO")]
        [DataMember]
        public string OFFNET_A_FIJO { get; set; }


        [Data.DbColumn("OFFNET_A_CELULAR")]
        [DataMember]
        public string OFFNET_A_CELULAR { get; set; }


        [Data.DbColumn("IMPORTE")]
        [DataMember]
        public string IMPORTE { get; set; }


        [Data.DbColumn("IMPORTE2")]
        [DataMember]
        public string IMPORTE2 { get; set; }

    }

}
