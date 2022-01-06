using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost
{
    [DataContract]
    public class ListTabPlanesMassivePostType
    {
        [DataMember(Name = "tabPlanesMasivoPost")]
        public List<TabPlanesMassivePostType> tabPlanesMasivoPost { get; set; }
    }
}
