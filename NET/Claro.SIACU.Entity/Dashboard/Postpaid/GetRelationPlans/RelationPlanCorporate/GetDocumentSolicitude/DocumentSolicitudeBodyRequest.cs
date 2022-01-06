using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude
{
    [DataContract]
    public class DocumentSolicitudeBodyRequest
    {
        [DataMember(Name = "usuario")]
        public string Usuario { get; set; }
        [DataMember(Name = "clave")]
        public string Clave { get; set; }
        [DataMember(Name = "ruta")]
        public string Ruta { get; set; }
        [DataMember(Name = "codigoAplicacion")]
        public string CodigoAplicacion { get; set; }
        [DataMember(Name = "puerto")]
        public string Puerto { get; set; }
        [DataMember(Name = "ip")]
        public string Ip { get; set; }
        [DataMember(Name = "nombreDocumento")]
        public string NombreDocumento { get; set; }
        [DataMember(Name = "extension")]
        public string Extension { get; set; }
        [DataMember(Name = "flag")]
        public string Flag { get; set; }
        [DataMember(Name = "cantidad")]
        public string Cantidad { get; set; }
    }
}
