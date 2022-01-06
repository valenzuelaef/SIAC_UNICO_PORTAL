using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class BagMain
    {
        [DataMember(Name = "tipoBolsa")]
        public string TypeBag { get; set; }
        [DataMember(Name = "fechaActivacion")]
        public string ActivationDate { get; set; }
        [DataMember(Name = "cargoFijo")]
        public string FixedCharge { get; set; }
        [DataMember(Name = "cantLineas")]
        public string NumberLines { get; set; }
        [DataMember(Name = "minSol")]
        public string MinSol { get; set; }
    }
}
