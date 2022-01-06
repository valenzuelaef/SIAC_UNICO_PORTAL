using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetInteraction
{
    [DataContract(Name = "InteractionRequestPostPaid")]
    public class InteractionRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Document { get; set; }
    }
}
