using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetValidateTelephone
{
    [DataContract(Name = "ValidateTelephoneResponsePrePaid")]
    public class ValidateTelephoneResponse
    {
        [DataMember]
        public string Cadena { get; set; }
    }
}
