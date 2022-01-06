using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class ListBagDetail
    {
        [DataMember(Name = "bolsaDetalle")]
        public List<BagDetail> BagDetail { get; set; }
    }
}
