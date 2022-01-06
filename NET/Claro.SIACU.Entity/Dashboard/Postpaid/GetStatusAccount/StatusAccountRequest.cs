using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccount
{
    [DataContract(Name = "StatusAccountRequestPostPaid")]
    public class StatusAccountRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }

        [DataMember]
        public string csIdPub { get; set; }

        [DataMember]
        public string plataformaAT { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string OriginType { get; set; }
        [DataMember]
        public bool isHR { get; set; }
    }
}
