using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetDebt
{
    [DataContract(Name = "DebtPrepaidResponse")]
    public class DebtResponse
    {
        [DataMember]
        public DebtSaleDues objDebtSaleDues { get; set; }
    }
}
