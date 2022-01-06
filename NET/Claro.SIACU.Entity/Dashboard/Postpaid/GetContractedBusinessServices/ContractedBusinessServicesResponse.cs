using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetContractedBusinessServices
{
    [DataContract(Name = "ContractedBusinessServicesResponsePostPaid")]
    public class ContractedBusinessServicesResponse
    {
        [DataMember]
        public List<ContractServices> ContractServices { get; set; }
        [DataMember]
        public List<PhoneContract> PhoneContracts { get; set; }
        [DataMember]
        public List<ServiceBSCS> BSCSServices { get; set; }
    }
}
