using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "OfficeAddressPostPaid")]
    public class OfficeAddress
    {
        
        [DataMember]
        [Data.DbColumn("RAZON_SOCIAL")]
        public string RAZON_SOCIAL { get; set; }
        
        [DataMember]
        [Data.DbColumn("DIRECCION_FACT")]
        public string DIRECCION_FACT { get; set; }
    }
}