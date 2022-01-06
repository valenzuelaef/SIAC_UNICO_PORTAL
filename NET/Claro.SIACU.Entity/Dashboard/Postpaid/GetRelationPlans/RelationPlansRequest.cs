using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans
{
    [DataContract(Name = "RelationPlansRequestPostPaid")]
    public class RelationPlansRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string TypeSearch { get; set; }
        [DataMember]
        public string Order { get; set; }
        [DataMember]
        public string AscDes { get; set; }
        [DataMember]
        public string Cycle { get; set; }
        [DataMember]
        public string ShBag { get; set; }
        
    }
}
