using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDueDocumentNumber
{
    [DataContract(Name = "DueDocumentNumberRequestPostPaid")]
    public class DueDocumentNumberRequest : Claro.Entity.Request
    {
        [DataMember]
        public string UserAplication { get; set; }

        [DataMember]
        public string DocumentIdentity { get; set; }

        [DataMember]
        public string NumberDocument { get; set; }
    }
}
