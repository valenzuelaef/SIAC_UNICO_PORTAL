using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class RegisterSolicitude
    {
        [DataMember(Name = "idsolicitud")]
        public string SolicitudeId { get; set; }
        [DataMember(Name = "resultado")]
        public string Result { get; set; }
        [DataMember(Name = "msg")]
        public string Msg { get; set; }
    }
}
