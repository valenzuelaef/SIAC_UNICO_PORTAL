using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetCustomerInfo
{
    [DataContract(Name = "CustomerInfoRequestDashboard")]
    public class CustomerInfoRequest : Claro.Entity.Request
    {

        [DataMember]
        public string SearchType { get; set; }
        [DataMember]
        public string SearchValue { get; set; }
        [DataMember]
        public OptionalObject ListOptionalObject { get; set; }

    }
}
