using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailLongDistance
{
    [DataContract(Name = "DetailLongDistanceResponsePostPaid")]
        public class DetailLongDistanceResponse
        {
            [DataMember]
            public List<CallDetail> ListCallDetail { get; set; }
        }
}
