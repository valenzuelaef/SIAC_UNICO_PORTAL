using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetOfficeAddress
{
    [DataContract(Name = "OfficeAddressRequestPostPaid")]
    public class OfficeAddressRequest : Claro.Entity.Request
    {
        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string DocumentNumber { get; set; }
    }
}
