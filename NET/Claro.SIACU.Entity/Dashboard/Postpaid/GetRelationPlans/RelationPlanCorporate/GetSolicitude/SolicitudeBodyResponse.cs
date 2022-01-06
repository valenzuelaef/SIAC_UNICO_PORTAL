using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude
{
    [DataContract]
    public class SolicitudeBodyResponse
    {
        [DataMember(Name = "responseStatus")]
        public Common.ResponseStatus ResponseStatus { get; set; }
        [DataMember(Name = "responseDataObtenerSolicitudes")]
        public ResponseDataGetSolicitude ResponseDataGetSolicitude { get; set; }
    }
}
