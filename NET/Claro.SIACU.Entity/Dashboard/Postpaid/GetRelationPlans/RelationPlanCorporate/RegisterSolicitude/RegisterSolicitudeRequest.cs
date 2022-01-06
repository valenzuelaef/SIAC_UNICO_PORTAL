using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class RegisterSolicitudeRequest
    {
        [DataMember(Name = "usuario")]
        public string User { get; set; }
        [DataMember(Name = "customerid")]
        public string CustomerId { get; set; }
        [DataMember(Name = "aplicativo")]
        public string Application { get; set; }
        [DataMember(Name = "lineasActivas")]
        public string ActiveLines { get; set; }
        [DataMember(Name = "cabeceraReporte")]
        public HeaderReport HeaderReport { get; set; }
    }
}
