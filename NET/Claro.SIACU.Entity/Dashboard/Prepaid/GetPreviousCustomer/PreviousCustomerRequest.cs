using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetPreviousCustomer
{
    [DataContract(Name = "PreviousCustomerRequestPrePaid")]
    public class PreviousCustomerRequest : Claro.Entity.Request
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
        public string IdTransaction { get; set; }
        [DataMember]
        public string IpApplication { get; set; }
        [DataMember]
        public string ApplicationName { get; set; }
        [DataMember]
        public string UserApplication { get; set; }
    }
}
