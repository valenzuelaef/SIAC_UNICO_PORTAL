using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Common.IsOkGetKey
{
    [DataContract(Name = "IsOkGetKeyResponseCommon")]
    public class IsOkGetKeyResponse
    {
        [DataMember]
        public bool result { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string Pass { get; set; }

    } 
}
