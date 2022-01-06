using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAmountDispute
{
    [DataContract(Name = "DetailAmountDisputeResponsePostPaid")]
    public class DetailAmountDisputeResponse
    {
        [DataMember]
        public List<DetailAmountDispute> ListDetailAmountDispute { get; set; }

    }
}
