using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "TelephoneZsansDataPrepaid")]
    public class TelephoneZsansData
    {
        [DataMember]
        public string NumeroTelefono { get; set; }
        [DataMember]
        public string MATNR { get; set; }
        [DataMember]
        public string SERNR { get; set; }
    }
}
