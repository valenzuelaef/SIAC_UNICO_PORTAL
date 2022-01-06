using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetPreviousCustomers
{
    [DataContract(Name = "PreviousCustomersResponsePrePaid")]
    public class PreviousCustomersResponse
    {
        [DataMember]
        public List<Customer> listCustomer { get; set; }
    }
}
