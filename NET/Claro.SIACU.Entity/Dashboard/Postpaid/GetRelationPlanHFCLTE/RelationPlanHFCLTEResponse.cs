using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE
{
    [DataContract(Name = "RelationPlanHFCLTEResponsePostPaid")]
    public class RelationPlanHFCLTEResponse
    {
   
        [DataMember]
        public List<Entity.Dashboard.Postpaid.Equipment> ListEquipment { get; set; }
        [DataMember]
        public List<Entity.Dashboard.Postpaid.ServiceGSMAccount> ListServiceGSMAccount { get; set; }
        [DataMember]
        public List<Entity.Dashboard.Postpaid.ServiceHFCAccount> ListServiceHFCAccount { get; set; }
        [DataMember]
        public List<Entity.Dashboard.Postpaid.ServiceLTEAccount> ListServiceLTEAccount { get; set; }



    }
}
