
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetPEL
{
    [DataContract(Name = "PELResponsePrePaid")]
    public class PELResponse
    {
        [DataMember]
        public Entity.Dashboard.Prepaid.PEL objPEL { get; set; }
    }
}
