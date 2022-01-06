using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetPreviousCustomers
{
    [DataContract(Name = "PreviousCustomersRequestPrePaid")]
    public class PreviousCustomersRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string ContactId { get; set; }
        [DataMember]
        public string FlagRegistry { get; set; }
        [DataMember]
        public string FlagGetAll { get; set; }
        [DataMember]
        public string FlagHistory { get; set; }
    }
}
