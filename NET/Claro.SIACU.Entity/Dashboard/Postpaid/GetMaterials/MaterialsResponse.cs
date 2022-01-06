using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetMaterials
{
    [DataContract(Name = "MaterialsResponsePostpaid")]
    public class MaterialsResponse
    {
        [DataMember]
        public List<Entity.Dashboard.Postpaid.Materials> ListMateriales { get; set; }
    }
}
