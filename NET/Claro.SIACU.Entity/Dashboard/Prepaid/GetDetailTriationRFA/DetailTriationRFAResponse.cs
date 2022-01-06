using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetDetailTriationRFA
{
    [DataContract(Name = "DetailTriationRFAResponse")]
    public class DetailTriationRFAResponse
    {
        [DataMember]
        public List<NumbersTriation> listNumbersTriation { get; set; }
    }
}
