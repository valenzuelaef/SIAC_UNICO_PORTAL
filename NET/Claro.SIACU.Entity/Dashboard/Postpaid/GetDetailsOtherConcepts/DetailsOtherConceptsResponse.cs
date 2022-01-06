using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailsOtherConcepts
{
    [DataContract(Name = "DetailsOtherConceptsResponsePostPaid")]
    public class DetailsOtherConceptsResponse
    {
        [DataMember]
        public List<OtherDetails> ListOtherDetails { get; set; }

    }
}

