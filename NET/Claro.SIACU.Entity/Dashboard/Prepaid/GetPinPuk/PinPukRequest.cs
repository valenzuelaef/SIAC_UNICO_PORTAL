using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetPinPuk
{
    [DataContract(Name = "PinPukRequestPrepaid")]
    public class PinPukRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string StarDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
    }
}