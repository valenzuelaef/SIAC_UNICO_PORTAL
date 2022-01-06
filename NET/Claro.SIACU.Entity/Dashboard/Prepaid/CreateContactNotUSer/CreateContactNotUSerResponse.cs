using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.CreateContactNotUSer
{
    [DataContract(Name = "CreateContactNotUSerResponsePrePaid")]
    public class CreateContactNotUSerResponse
    {
        [DataMember]
        public string FlagUpdate { get; set; }
        [DataMember]
        public string MessageText { get; set; }
        [DataMember]
        public bool Response { get; set; }

    }
}
