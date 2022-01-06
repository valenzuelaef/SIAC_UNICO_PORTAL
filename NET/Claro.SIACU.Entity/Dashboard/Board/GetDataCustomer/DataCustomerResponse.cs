using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetDataCustomer
{
    [DataContract(Name = "DataCustomerResponseDashboard")]
    public class DataCustomerResponse
    {
        [DataMember]
        public Entity.Dashboard.Postpaid.Customer ObjCustomer { get; set; }
    }
}
