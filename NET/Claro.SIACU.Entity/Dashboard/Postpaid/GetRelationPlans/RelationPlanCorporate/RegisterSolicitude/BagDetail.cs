using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class BagDetail
    {
        [DataMember(Name = "bolsa")]
        public string Bag { get; set; }
        [DataMember(Name = "cargo")]
        public string Charge { get; set; }
        [DataMember(Name = "fechaActivacion")]
        public string ActivationDate { get; set; }
        [DataMember(Name = "unidades")]
        public string Units { get; set; }
        [DataMember(Name = "cantidadLineas")]
        public string QuantityLines { get; set; }
    }
}
