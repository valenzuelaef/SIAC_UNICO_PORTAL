using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude
{
    [DataContract]
    public class SolicitudeMessageRequest
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public SolicitudeBodyRequest Body { get; set; }

        public SolicitudeMessageRequest()
        {
            Header = new Common.GetDataPower.HeadersRequest();
            Body = new SolicitudeBodyRequest();
        }
    }
}
