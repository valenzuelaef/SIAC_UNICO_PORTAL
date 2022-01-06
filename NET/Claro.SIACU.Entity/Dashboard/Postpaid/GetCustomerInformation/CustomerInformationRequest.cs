using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomerInformation
{
    [DataContract(Name = "CustomerInformationRequestPostPaid")]
    public class CustomerInformationRequest : Claro.Entity.Request
    {
        [DataMember]
        public double IdCaso { get; set; }
    }
}
