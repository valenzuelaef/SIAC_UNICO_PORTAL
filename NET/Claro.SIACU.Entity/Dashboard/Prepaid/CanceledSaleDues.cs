using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "CanceledSaleDuesPrepaid")]
    public class CanceledSaleDues
    {
        [DataMember]
        public string BKTXT { get; set; }
        [DataMember]
        public string FECHA { get; set; }
        [DataMember]
        public string MONTO { get; set; }
        [DataMember]
        public string XBLNR { get; set; }
        [DataMember]
        public string FECHAPAGO { get; set; }
    }
}
