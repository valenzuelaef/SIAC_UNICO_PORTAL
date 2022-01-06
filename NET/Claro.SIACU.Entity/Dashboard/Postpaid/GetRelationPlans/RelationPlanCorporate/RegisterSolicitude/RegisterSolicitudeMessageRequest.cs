using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class RegisterSolicitudeMessageRequest
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public RegisterSolicitudeBodyRequest Body { get; set; }

        public RegisterSolicitudeMessageRequest()
        {
            Header = new Common.GetDataPower.HeadersRequest();
            Body = new RegisterSolicitudeBodyRequest();
        }
    }
}
