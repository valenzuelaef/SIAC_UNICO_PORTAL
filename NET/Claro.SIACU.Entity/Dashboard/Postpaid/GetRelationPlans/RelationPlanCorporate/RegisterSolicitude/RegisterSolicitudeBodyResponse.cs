using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class RegisterSolicitudeBodyResponse
    {
        [DataMember(Name = "responseStatus")]
        public Common.ResponseStatus ResponseStatus { get; set; }
        [DataMember(Name = "registrarSolicitudPendienteResponse")]
        public RegisterSolicitudePendingResponse RegisterSolicitudePending { get; set; }
        [DataMember(Name = "registrarSolicitudResponse")]
        public RegisterSolicitude RegisterSolicitude { get; set; }
    }
}
