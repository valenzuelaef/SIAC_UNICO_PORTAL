using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class RegisterSolicitudeMessageResponse
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public RegisterSolicitudeBodyResponse Body { get; set; }

        public RegisterSolicitudeMessageResponse()
        {
            Header = new Common.GetDataPower.HeadersResponse();
            Body = new RegisterSolicitudeBodyResponse();
        }
    }
}
