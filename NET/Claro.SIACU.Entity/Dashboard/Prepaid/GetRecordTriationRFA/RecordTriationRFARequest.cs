using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetRecordTriationRFA
{
    [DataContract(Name = "RecordTriationRFARequest")]
    public class RecordTriationRFARequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string DateStart { get; set; }
        [DataMember]
        public string DateEnd { get; set; }
        [DataMember]
        public string PlanTariff { get; set; }
    }
}
