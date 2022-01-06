using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount
{
    [DataContract]
    public class responseDataConsultar
    {
        public responseDataConsultar()
        {
            debtNotExpired = new DebtNotExpired();
            debtExpired = new DebtExpired();
            ListReceivableDeferred = new List<ReceivableDeferred>();
            ListClaimByReceipt = new List<ClaimByReceipt>();            
        }

        [DataMember(Name = "deudaNoVencida")]
        public DebtNotExpired debtNotExpired { get; set; }

        [DataMember(Name = "deudaVencida")]
        public DebtExpired debtExpired { get; set; }

        [DataMember(Name = "listaCobranzaDiferida")]
        public List<ReceivableDeferred> ListReceivableDeferred { get; set; }

        [DataMember(Name = "listaReclamoPorRecibo")]
        public List<ClaimByReceipt> ListClaimByReceipt { get; set; }
    }
}
