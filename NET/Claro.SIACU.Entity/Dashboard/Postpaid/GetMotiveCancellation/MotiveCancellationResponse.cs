using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetMotiveCancellation
{
    [DataContract]
    public class MotiveCancellationResponse
    {
        [DataMember]
        public string FlagResult { get; set; }
        [DataMember]
        public string MotiveCancellation { get; set; }
        [DataMember]
        public string CodeError { get; set; }
        [DataMember]
        public string DescriptionError { get; set; }
    }
}
