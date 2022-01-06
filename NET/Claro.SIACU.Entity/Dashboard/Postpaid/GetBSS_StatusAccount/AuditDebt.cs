using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount
{
    [DataContract(Name = "audit")]
    public class AuditDebt
    {
        [DataMember(Name = "txId")]
        public string txId { get; set; }

        [DataMember(Name = "errorCode")]
        public string errorCode { get; set; }

        [DataMember(Name = "errorMsg")]
        public string errorMsg { get; set; }
    }
}
