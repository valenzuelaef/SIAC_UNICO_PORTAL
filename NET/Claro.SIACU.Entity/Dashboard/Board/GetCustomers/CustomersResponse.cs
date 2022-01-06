using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetCustomers
{
    [DataContract(Name = "CustomersResponse")]
    public class CustomersResponse
    {
        [DataMember]
        public List<Entity.Dashboard.Person> ListPerson { get; set; }
    }
}
