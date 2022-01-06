using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Response
{
    [DataContract]
    public class ListaContratos
    {
        [DataMember]
        public string csCode { get; set; }
        [DataMember]
        public string adrFullname { get; set; }
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public string coStatus { get; set; }
        [DataMember]
        public string coActivated { get; set; }
        [DataMember]
        public string coLastStatusChangeDate { get; set; }
        [DataMember]
        public string contractIdPub { get; set; }
        [DataMember]
        public string desCoStatus { get; set; }
        [DataMember]
        public string origen { get; set; }  //0-> BSCS7, 1-> BSCS9 
        [DataMember]
        public List<ListaReason> reason { get; set; }


    }
}
