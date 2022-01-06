using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Coliving.getListOST
{
    [DataContract(Name = "ListOSTRequestColiving")]
    public class ListOSTRequest : Claro.Entity.Request
    {
        [DataMember]
        public string IdBusca { get; set; }
        [DataMember]
        public string IdCriterio { get; set; }
        [DataMember]
        public string IdEstado { get; set; }
        [DataMember]
        public string IdCAC { get; set; }
        
    }
}
