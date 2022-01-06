using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryEquipments
{
    [DataContract(Name = "DecoResponsePostPaid")]
    public class DecoResponse
    {
        [DataMember]
        public List<Deco> ListDeco { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Title { get; set; } 
    }
}
