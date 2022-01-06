using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetPinPuk
{
    [DataContract(Name = "PinPukRequestPostPaid")]
    public class PinPukRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string Type { get; set; }

    }
}
