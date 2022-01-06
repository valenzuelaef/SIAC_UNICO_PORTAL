using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude
{
    [DataContract]
    public class GetSolicitude
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "usuario")]
        public string User { get; set; }

        [DataMember(Name = "fechaSolicitud")]
        public string SolicitudeDate { get; set; }

        [DataMember(Name = "fechaTermino")]
        public string EndDate { get; set; }

        [DataMember(Name = "customerid")]
        public string CustomerId { get; set; }

        [DataMember(Name = "lineasActivas")]
        public string ActiveLines { get; set; }

        [DataMember(Name = "aplicativo")]
        public string Application { get; set; }

        [DataMember(Name = "nombreArchivo")]
        public string FileName { get; set; }

        [DataMember(Name = "estado")]
        public string State { get; set; }
    }
}
