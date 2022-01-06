using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetCustomerName
{
    [DataContract(Name = "CustomerNameResponseDashboard")]
    public class CustomerNameResponse
    {
        public CustomerNameResponse()
        {
            ListPerson = new List<Person>();
            ListOptionalObject = new List<OptionalObject>();
        }

        [DataMember]
        public List<Person> ListPerson { get; set; }
        [DataMember]
        public List<OptionalObject> ListOptionalObject { get; set; }


        [DataMember]
        public string ErrorMsg { get; set; }
        [DataMember]
        public string CodeError { get; set; }
    }
}
