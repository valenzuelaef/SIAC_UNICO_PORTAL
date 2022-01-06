using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetAnnotations
{
    [DataContract(Name = "AnnotationsRequestPostPaid")]
    public class AnnotationsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string Contract { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string NumberTickler { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string plataforma { get; set; }
        [DataMember]
        public string flagconvivencia { get; set; }
    }
}
