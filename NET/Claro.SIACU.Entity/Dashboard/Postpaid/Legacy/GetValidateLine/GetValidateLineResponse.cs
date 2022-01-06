using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetValidateLine
{
    [DataContract]
    public class GetValidateLineResponse
    {
        [DataMember]
        public validarLineaResponse validarLineaResponse { get; set; }
    }
}
