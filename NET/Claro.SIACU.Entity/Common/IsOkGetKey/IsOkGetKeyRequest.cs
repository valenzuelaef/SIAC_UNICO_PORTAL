using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Common.IsOkGetKey
{
    [DataContract(Name = "IsOkGetKeyRequestCommon")]
    public class IsOkGetKeyRequest : Claro.Entity.Request
    {
        public string KeyValue { get; set; }
    }
}
