
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetPEL
{
    [DataContract(Name = "PELRequestPrePaid")]
    public class PELRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
    }
}
