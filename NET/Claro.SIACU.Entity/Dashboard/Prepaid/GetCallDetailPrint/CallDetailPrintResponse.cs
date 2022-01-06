using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetCallDetailPrint
{
    [DataContract(Name = "CallDetailPrintResponsePrePaid")]
    public class CallDetailPrintResponse
    {
        [DataMember]
        public List<Call> listCall { get; set; }
    }
}
