using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class RegisterSolicitudeRequests : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public RegisterSolicitudeMessageRequest MessageRequest { get; set; }

        public RegisterSolicitudeRequests()
        {
            MessageRequest = new RegisterSolicitudeMessageRequest();
        }
    }
}
