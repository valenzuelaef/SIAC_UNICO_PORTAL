using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts
{
    [DataContract(Name = "PostpaidProductsRequestDashboard")]
    public class PostpaidProductsRequest : Claro.Entity.Request
    {

        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string DocumentIdentity { get; set; }
        [DataMember]
        public OptionalObject ListOptionalObject { get; set; }
    }
}
