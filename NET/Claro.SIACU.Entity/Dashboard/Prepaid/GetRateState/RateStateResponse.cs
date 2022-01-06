using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetRateState
{
    [DataContract]
    public class RateStateResponse
    {
        [DataMember]
        public string codRespuesta { get; set; }
        [DataMember]
        public string msjRespuesta { get; set; }
        [DataMember]
        public string estado { get; set; }


    }
}
