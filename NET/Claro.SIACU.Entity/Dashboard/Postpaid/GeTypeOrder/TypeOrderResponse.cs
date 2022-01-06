using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GeTypeOrder
{
    [DataContract(Name = "TypeOrderResponseCommon")]
    public class TypeOrderResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}


