using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Management.GetListInstant
{
    [DataContract(Name = "ListInstantRequestManagement")]
    public class ListInstantRequest : Claro.Entity.Request
    {

        [DataMember]
        public string vDato { get; set; }
        [DataMember]
        public string vTipoTelefono { get; set; }
        [DataMember]
        public string fecha { get; set; }
    }
}
