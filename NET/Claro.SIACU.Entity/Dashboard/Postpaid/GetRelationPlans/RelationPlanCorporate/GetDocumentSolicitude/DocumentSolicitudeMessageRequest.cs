using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude
{
    [DataContract]
    public class DocumentSolicitudeMessageRequest
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public DocumentSolicitudeBodyRequest Body { get; set; }

        public DocumentSolicitudeMessageRequest()
        {
            Header = new Common.GetDataPower.HeadersRequest();
            Body = new DocumentSolicitudeBodyRequest();
        }
    }
}
