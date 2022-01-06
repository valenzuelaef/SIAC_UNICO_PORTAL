using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetPostpaidLines
{
    [DataContract(Name = "PostpaidLinesRequestDashboard")]
    public class PostpaidLinesRequest : Claro.Entity.Request
    {

        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string CodeProduct { get; set; }
        [DataMember]
        public string IdPlan { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Origin { get; set; }
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public OptionalObject ListOptionalObject { get; set; }

    }
}
