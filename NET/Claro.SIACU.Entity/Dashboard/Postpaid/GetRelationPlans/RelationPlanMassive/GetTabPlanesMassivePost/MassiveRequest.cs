using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost
{
    [DataContract(Name = "MassiveRequestPostPaid")]
    public class MassiveRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public MassiveMessageRequest MessageRequest { get; set; }
        [DataMember(Name = "Account")]
        public string Account { get; set; }
        [DataMember(Name = "TypeSearch")]
        public string TypeSearch { get; set; }
        [DataMember(Name = "Order")]
        public string Order { get; set; }
        [DataMember(Name = "AscDes")]
        public string AscDes { get; set; }
        [DataMember(Name = "ShBag")]
        public string ShBag { get; set; }
        [DataMember(Name = "Cycle")]
        public string Cycle { get; set; }
        [DataMember(Name = "CustomerType")]
        public string CustomerType { get; set; }
    }
}
