using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetComputerQuery
{
    [DataContract(Name = "ComputerQueryRequestPostPaid")]
    public class ComputerQueryRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public string ContractID { get; set; }
    }
}
