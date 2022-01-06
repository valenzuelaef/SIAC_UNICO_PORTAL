using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSuspendedContract
{
    [DataContract(Name = "SuspendedContractRequestPostPaid")]
    public class SuspendedContractRequest : Claro.Entity.Request
    {

        [DataMember]
        public string StartDate {get; set;}
        [DataMember]
        public string EndDate {get; set;}
        [DataMember]
        public string ReasonID {get; set;}
 

    }
}
