using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer
{
    [DataContract(Name = "CustomerResponsePostPaid")]
    public class CustomerResponse
    {
        [DataMember]
        public Entity.Dashboard.Postpaid.Customer ObjCustomer { get; set; }
    }
}
