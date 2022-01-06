using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class RegisterSolicitudeBodyRequest
    {
        [DataMember(Name = "registrarSolicitudPendiente")]
        public RegisterSolicitudePendingRequest RegisterSolicitudePending { get; set; }
        [DataMember(Name = "registrarSolicitud")]
        public RegisterSolicitudeRequest RegisterSolicitude { get; set; }
    }
}
