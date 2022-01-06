using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetInteraction
{
    [DataContract(Name = "InteractionResponsePostPaid")]
    public class InteractionResponse
    {
        [DataMember]
        public Int64 IdInteraction { get; set; }
    }
}
