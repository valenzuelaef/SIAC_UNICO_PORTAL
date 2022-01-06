using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Common
{
    [DataContract]
    public class ListaOpcional
    {
        [DataMember]
        public string clave { get; set; }

        [DataMember]
        public string valor { get; set; }

    }
}
