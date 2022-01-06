using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistorySIM
{
    [DataContract(Name = "HistorySIMRequestPostPaid")]
    public class HistorySIMRequest : Claro.Entity.Request
    {
        [DataMember]
        public string ContractID { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string ResponseCode { get; set; }
        [DataMember]
        public string Response { get; set; }
        
        [DataMember]
        public string strPlataforma { get; set; }
        [DataMember]
        public string strFechaMigracion { get; set; }
        [DataMember]
        public string flagconvivencia { get; set; }

       

    }
}
