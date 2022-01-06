using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude
{
    [DataContract]
    public class DocumentSolicitudeBodyResponse
    {
        [DataMember(Name = "mensajeError")]
        public string MensajeError { get; set; }
        [DataMember(Name = "codigoRespuesta")]
        public string CodigoRespuesta { get; set; }
        [DataMember(Name = "mensajeRespuesta")]
        public string MensajeRespuesta { get; set; }
        [DataMember(Name = "reporte")]
        public byte[] Reporte { get; set; }
    }
}
