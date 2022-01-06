using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude
{
    [DataContract]
    public class SolicitudeResponse
    {
        [DataMember(Name = "MessageResponse")]
        public SolicitudeMessageResponse MessageResponse { get; set; }
    }
}
