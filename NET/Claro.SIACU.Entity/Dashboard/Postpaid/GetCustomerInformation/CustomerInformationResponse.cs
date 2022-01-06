using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomerInformation
{
    [DataContract(Name = "CustomerInformationResponsePostPaid")]
    public class CustomerInformationResponse
    {
        [DataMember]
        public QueryOAC QueryOAC { get; set; }
    }
}
