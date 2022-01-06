using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class RegisterSolicitudeResponse
    {
        [DataMember(Name = "MessageResponse")]
        public RegisterSolicitudeMessageResponse MessageResponse { get; set; }
    }
}
