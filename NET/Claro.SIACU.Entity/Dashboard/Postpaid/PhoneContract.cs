using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Response;
using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "PhoneContractPostPaid")]
    public class PhoneContract
    {
        [DataMember]
        public string CUSTCODE { get; set; }
        [DataMember]
        public string NOMBRE { get; set; }
        [DataMember]
        public string COID { get; set; }
        [DataMember]
        public string COID_PUB { get; set; }
        [DataMember]
        public string ESTADO { get; set; }
        [DataMember]
        public string FECHA { get; set; }
        [DataMember]
        public string RAZON { get; set; }
        [DataMember]
        public string plataformaAT { get; set; }
        [DataMember]
        public string rsDes { get; set; }
        [DataMember]
        public string rsCode { get; set; }
        [DataMember]
        public string rsState { get; set; }
        [DataMember]
        public string origen { get; set; }
    }
}
