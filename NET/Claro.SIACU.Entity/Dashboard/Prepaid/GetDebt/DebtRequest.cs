using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetDebt
{
    [DataContract(Name = "DebtPrepaidRequest")]
    public class DebtRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Bukrs { get; set; }
        [DataMember]
        public string Telephone { get; set; }
    }
}
