using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Response
{
    [DataContract]
    public class ListaReason
    {
        [DataMember]
        public string rsCode { get; set; }
        [DataMember]
        public string rsDes { get; set; }
        [DataMember]
        public string rsState { get; set; }

    }
}
