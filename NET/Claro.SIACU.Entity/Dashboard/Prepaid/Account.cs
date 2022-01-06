using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "AccountPrepaid")]
    public class Account
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Saldo { get; set; }
        [DataMember]
        public string FechaExpiracion { get; set; }
        [DataMember]
        public string Orden { get; set; }

    }
}
