using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetPrepaidProducts
{
    [DataContract(Name = "PrepaidProductsRequestDashboard")]
    public class PrepaidProductsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string DocumentIdentity { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public OptionalObject ListOptionalObject { get; set; }

    }
}
