using Claro.SIACU.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetPortability
{
    [DataContract(Name = "PortabilityResponseCommon")]
    public class PortabilityResponse
    {
        [DataMember]
        public List<Portability> ListPortability { get; set; }
        [DataMember]
        public string Respuesta { get; set; }
        [DataMember]
        public string TypeProcessDate { get; set; }
        [DataMember]
        public string TypeProcessOperator { get; set; }
        [DataMember]
        public DateTime ExecutionDate { get; set; }
        [DataMember]
        public string Operator { get; set; }
        [DataMember]
        public Boolean TrTypeProcessDate { get; set; }
        [DataMember]
        public Boolean TrTypeProcessOperator { get; set; }
        [DataMember]
        public string ApplicationNumber { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public DateTime RegistrationDate { get; set; }
    }
}