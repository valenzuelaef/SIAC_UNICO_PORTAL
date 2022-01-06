using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryDeliveryToBe.Response
{
    [DataContract]
    public class P_CURSOR
    {
        [DataMember]
        public List<P_CURSOR_ROW> P_CURSOR_Row { get; set; }
    }
}
