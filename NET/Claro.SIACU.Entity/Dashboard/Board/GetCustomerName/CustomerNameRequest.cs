using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetCustomerName
{
    [DataContract(Name = "CustomerNameRequestDashboard")]
    public class CustomerNameRequest : Claro.Entity.Request
    {

        [DataMember]
        public string SearchType { get; set; }
        [DataMember]
        public string SearchValue { get; set; }
        [DataMember]
        public OptionalObject ListOptionalObject { get; set; }

    }
}
