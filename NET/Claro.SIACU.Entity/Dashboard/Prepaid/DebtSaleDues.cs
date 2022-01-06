using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "DebtSaleDuesPrepaid")]
    public class DebtSaleDues
    {
        [DataMember]
        public string KUNNR { get; set; }
        [DataMember]
        public decimal Mwsbk { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Netwr { get; set; }
        [DataMember]
        public string Stcd1 { get; set; }
        [DataMember]
        public decimal Total { get; set; }
        [DataMember]
        public List<PendingSaleDues> listPendingSaleDues { get; set; }
        [DataMember]
        public List<CanceledSaleDues> listCanceledSaleDues { get; set; }

        public DebtSaleDues()
        {
            listPendingSaleDues = new List<PendingSaleDues>();
            listCanceledSaleDues = new List<CanceledSaleDues>();
        }
    }
}
