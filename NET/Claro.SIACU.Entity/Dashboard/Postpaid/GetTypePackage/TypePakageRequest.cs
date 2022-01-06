using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTypePackage
{
    [DataContract(Name = "TypePakageRequestCommon")]
    public class TypePakageRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Plataforma { get; set; }
    }
}
