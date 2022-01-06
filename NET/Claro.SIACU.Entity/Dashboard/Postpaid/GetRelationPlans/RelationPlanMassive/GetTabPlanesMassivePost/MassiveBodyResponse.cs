using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost
{
    [DataContract]
    public class MassiveBodyResponse
    {
        [DataMember(Name = "responseStatus")]
        public Common.ResponseStatus responseStatus { get; set; }
        [DataMember(Name = "responseDataObtenerTabPlanesMasivoPost")]
        public ResponseDataGetTabPlanesMassivePostType responseDataObtenerTabPlanesMasivoPost { get; set; }
    }
}
