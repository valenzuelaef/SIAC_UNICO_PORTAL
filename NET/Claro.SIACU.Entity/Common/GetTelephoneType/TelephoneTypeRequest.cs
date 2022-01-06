using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetTelephoneType
{
    [DataContract(Name = "TelephoneTypeRequestCommon")]
    public class TelephoneTypeRequest : Claro.Entity.Request
    {
        [DataMember]
        public int ContractId { get; set; }
        [DataMember]
        public string Application { get; set; }
    }
}
