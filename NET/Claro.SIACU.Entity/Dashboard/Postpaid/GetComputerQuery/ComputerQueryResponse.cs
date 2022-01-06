using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetComputerQuery
{
    [DataContract(Name = "ComputerQueryResponsePostPaid")]
    public class ComputerQueryResponse
    {
        [DataMember]
        public List<Deco> Decos { get; set; }
    }
}
