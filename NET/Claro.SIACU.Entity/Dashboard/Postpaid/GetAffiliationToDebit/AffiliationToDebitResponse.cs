using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetAffiliationToDebit
{
    [DataContract(Name = "AffiliationToDebitResponsePostPaid")]
    public class AffiliationToDebitResponse
    {
        [DataMember]
        public Entity.Dashboard.Postpaid.AffiliationToDebit ObjMethodPayment { get; set; }
    }
}
