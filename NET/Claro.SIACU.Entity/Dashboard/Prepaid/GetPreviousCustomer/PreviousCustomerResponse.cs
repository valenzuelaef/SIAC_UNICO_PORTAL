using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetPreviousCustomer
{
    [DataContract(Name = "PreviousCustomerResponsePrePaid")]
    public class PreviousCustomerResponse
    {
        [DataMember]
        public Entity.Dashboard.Prepaid.Customer objCustomer { get; set; }

        [DataMember]
        public string CodeResponse { get; set; }
    }
}
