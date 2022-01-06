using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetPinPuk
{
    [DataContract(Name = "PinPukResponsePostPaid")]
    public class PinPukResponse
    {
        [DataMember]
        public List<PinPuk> ListPinPuk { get; set; }

    }
}
