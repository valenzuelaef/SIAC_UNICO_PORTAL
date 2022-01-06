using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetDetailTriationRFA
{
    [DataContract(Name = "DetailTriationRFARequest")]
    public class DetailTriationRFARequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string IdInteraction { get; set; }
    }
}
