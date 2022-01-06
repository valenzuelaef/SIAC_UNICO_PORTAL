using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetPinPuk
{
    [DataContract(Name = "PinPukResponsePrepaid")]
    public class PinPukResponse
    {
        [DataMember]
        public List<PinPuk> listPinPuk { get; set; }
        [DataMember]
        public string Respuesta { get; set; }
        [DataMember]
        public string BlackList { get; set; }

    }
}
