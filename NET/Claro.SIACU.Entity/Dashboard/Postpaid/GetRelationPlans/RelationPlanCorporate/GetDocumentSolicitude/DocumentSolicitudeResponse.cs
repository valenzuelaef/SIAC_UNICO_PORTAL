using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude
{
    [DataContract]
    public class DocumentSolicitudeResponse
    {
        [DataMember(Name = "MessageResponse")]
        public DocumentSolicitudeMessageResponse MessageResponse { get; set; }
    }
}
