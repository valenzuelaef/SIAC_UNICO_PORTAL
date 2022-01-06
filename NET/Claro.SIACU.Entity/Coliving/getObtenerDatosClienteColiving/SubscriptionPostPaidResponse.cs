using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Coliving.getObtenerDatosClienteColiving
{
    [DataContract]
    public class SubscriptionPostPaidResponse
    {
        [DataMember]
        public String Code_Plan { get; set; }
        [DataMember]
        public String CustomerId { get; set; }
        [DataMember]
        public String Description { get; set; }
        [DataMember]
        public String ContractNumber { get; set; }
        [DataMember]
        public String Code_PlanType { get; set; }
        [DataMember]
        public String Code_AccountType { get; set; }
        [DataMember]
        public String Segment { get; set; }
        [DataMember]
        public String AccountNumber { get; set; }
        [DataMember]
        public String LineNumber { get; set; }
        [DataMember]
        public String ProductType { get; set; }
        [DataMember]
        public String LineStatus { get; set; }
        [DataMember]
        public String RatePlan { get; set; }
        [DataMember]
        public String OrigenInfo { get; set; }
        [DataMember]
        public string MigrateOne { get; set; }
    }
}
