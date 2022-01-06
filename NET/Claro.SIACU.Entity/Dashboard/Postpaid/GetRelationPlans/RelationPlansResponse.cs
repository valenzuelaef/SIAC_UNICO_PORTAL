using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans
{
    [DataContract(Name = "RelationPlansResponsePostPaid")]
    public class RelationPlansResponse
    {
        [DataMember]
        public Entity.Dashboard.Postpaid.Bag ObjBag { get; set; }
        [DataMember]
        public List<Entity.Dashboard.Postpaid.BagDetail> ListBagDetail { get; set; }
        [DataMember]
        public GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost.MassiveResponse ListPlan { get; set; }
    }
}
