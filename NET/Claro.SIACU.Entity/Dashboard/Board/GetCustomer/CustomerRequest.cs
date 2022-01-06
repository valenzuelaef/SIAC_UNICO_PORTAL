using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetCustomer
{
    [DataContract(Name = "CustomerRequestDashboard")]
    public class CustomerRequest : Claro.Entity.Request
    {
        [DataMember]
        public string TypeSearch { get; set; }
        [DataMember]
        public string ValueSearch { get; set; }
        [DataMember]
        public string ApplicationType { get; set; }
        [DataMember]
        public bool NotEvalState { get; set; }
        [DataMember]
        public bool FlagSearchType { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public bool IsPrepaid { get; set; }
        [DataMember]
        public bool IsPostPaid { get; set; }

    }
}
