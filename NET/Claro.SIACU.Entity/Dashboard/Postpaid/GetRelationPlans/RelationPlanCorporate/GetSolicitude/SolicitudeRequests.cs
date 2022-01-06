using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude
{
    [DataContract]
    public class SolicitudeRequests : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public SolicitudeMessageRequest MessageRequest { get; set; }

        public SolicitudeRequests()
        {
            MessageRequest = new SolicitudeMessageRequest();
        }
    }
}
