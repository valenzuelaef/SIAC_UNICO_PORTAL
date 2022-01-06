using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetValidateTelephone
{
    [DataContract(Name = "ValidateTelephoneRequestPrePaid")]
    public class ValidateTelephoneRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public bool TFI { get; set; }
        [DataMember]
        public string ProviderID { get; set; }
        [DataMember]
        public string CodigoResponse { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
    }
}
