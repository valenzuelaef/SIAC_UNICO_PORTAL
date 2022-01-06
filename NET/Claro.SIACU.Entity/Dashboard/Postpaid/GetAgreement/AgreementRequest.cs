using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetAgreement
{
    [DataContract(Name = "AgreementRequestPostPaid")]
        public class AgreementRequest : Claro.Entity.Request
        {
            [DataMember]
            public string Type { get; set; }

            [DataMember]
            public string Value { get; set; }

        }
  
}
