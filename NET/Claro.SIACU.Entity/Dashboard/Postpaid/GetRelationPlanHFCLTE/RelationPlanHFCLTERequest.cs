using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE
{
    [DataContract(Name = "RelationPlanHFCLTERequestPostPaid")]
    public class RelationPlanHFCLTERequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string Aplication { get; set; }

    }
}
