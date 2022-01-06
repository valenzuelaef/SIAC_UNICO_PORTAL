using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFeeEquipment
{
    [DataContract(Name = "FeeEquipmentRequestPostPaid")]
    public class FeeEquipmentRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Origin { get; set; }

        [DataMember]
        public string CustomerId { get; set; }

        [DataMember]
        public string ErrorId{ get; set; }

        [DataMember]
        public string Errormessage { get; set; }

    }
}
