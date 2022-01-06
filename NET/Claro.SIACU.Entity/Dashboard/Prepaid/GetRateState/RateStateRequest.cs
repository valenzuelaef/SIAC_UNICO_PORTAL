using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetRateState
{
   [DataContract]
    public class RateStateRequest: Claro.Entity.Request
    {
       [DataMember]
        public string strPhoneNumber { get; set;  }
    }
}
