using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DbTable("TEMPO")]
    public class SegmentDescription
    {
        [DbColumn("BENEV_DESCRIPCION")]
        [DataMember]
        public string Descripcion { get; set; }
    }
}
