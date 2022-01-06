using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailsOtherConcepts
{
    [DataContract(Name = "DetailsOtherConceptsRequestPostPaid")]
    public class DetailsOtherConceptsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}

