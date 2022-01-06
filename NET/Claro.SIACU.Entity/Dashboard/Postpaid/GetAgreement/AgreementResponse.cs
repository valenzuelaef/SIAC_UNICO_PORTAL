using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetAgreement
{

    [DataContract(Name = "AgreementeResponsePostPaid")]
        public class AgreementResponse
        {
            [DataMember]
            public bool Result { get; set; }

        } 
    
}
