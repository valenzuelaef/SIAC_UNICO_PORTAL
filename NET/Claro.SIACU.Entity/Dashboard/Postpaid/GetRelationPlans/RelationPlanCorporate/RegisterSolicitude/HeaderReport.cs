using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class HeaderReport
    {
        [DataMember(Name = "datosCliente")]
        public CustomerData CustomerData { get; set; }
        [DataMember(Name = "bolsaPrincipal")]
        public BagMain BagMain { get; set; }
        [DataMember(Name = "listaBolsaDetalle")]
        public ListBagDetail ListBagDetail { get; set; }
    }
}
