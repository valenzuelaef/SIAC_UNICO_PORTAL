using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryActions
{
    [DataContract(Name = "HistoryActionsRequestPostPaid")]
    public class HistoryActionsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public int Contract { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
        [DataMember]
        public string plataform { get; set; }
        [DataMember]
        public string flagConvivencia { get; set; }
        [DataMember]
        public string TelephoneTOBE { get; set; }
    }
}