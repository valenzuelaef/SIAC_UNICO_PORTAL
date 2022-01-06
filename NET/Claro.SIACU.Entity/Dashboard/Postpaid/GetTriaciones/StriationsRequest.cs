using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTriaciones
{
    [DataContract(Name = "StriationsRequestPostPaid")]
    public class StriationsRequest : Claro.Entity.Request
    {
        [DataMember]
        public int ContractId { get; set; }
    }
}
