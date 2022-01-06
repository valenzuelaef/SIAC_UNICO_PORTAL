using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetCustomers
{
    [DataContract(Name = "CustomersRequest")]
    public class CustomersRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string FlagQuery { get; set; }
        [DataMember]
        public string MesageText { get; set; }
        [DataMember]
        public Int16 ModeQuery { get; set; }
        [DataMember]
        public  List<Entity.Dashboard.Person> ListCustomerSearch  { get; set; }

    }
}
