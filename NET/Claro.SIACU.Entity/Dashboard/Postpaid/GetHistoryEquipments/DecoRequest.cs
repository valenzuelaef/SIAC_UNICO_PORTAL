using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryEquipments
{
    [DataContract(Name = "DecoRequestPostPaid")]
    public class DecoRequest : Claro.Entity.Request
    {
        [DataMember]
        public  string ContractID{ get; set; }
        [DataMember]
        public  string CustomerID { get; set; }
        [DataMember]
        public string  Application { get; set; }
    }
} 
