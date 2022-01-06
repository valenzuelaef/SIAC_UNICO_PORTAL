using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetParameterTerminalTPI
{
    [DataContract(Name = "ParameterTerminalTPIRequestPostPaid")]
    public class ParameterTerminalTPIRequest : Claro.Entity.Request
    {
        [DataMember]
        public string codeParameter { get; set; }
    }
}
