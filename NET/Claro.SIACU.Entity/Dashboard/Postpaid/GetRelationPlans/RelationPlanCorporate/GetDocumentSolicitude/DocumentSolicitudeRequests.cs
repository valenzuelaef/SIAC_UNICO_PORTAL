using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude
{
    [DataContract]
    public class DocumentSolicitudeRequests : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public DocumentSolicitudeMessageRequest MessageRequest { get; set; }

        public DocumentSolicitudeRequests()
        {
            MessageRequest = new DocumentSolicitudeMessageRequest();
        }
    }
}
