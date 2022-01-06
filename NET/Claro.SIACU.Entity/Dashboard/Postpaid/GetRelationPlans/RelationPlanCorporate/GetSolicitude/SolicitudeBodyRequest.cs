using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude
{
    [DataContract]
    public class SolicitudeBodyRequest
    {
        [DataMember(Name = "customerid")]
        public string CustomerId { get; set; }
    }
}
