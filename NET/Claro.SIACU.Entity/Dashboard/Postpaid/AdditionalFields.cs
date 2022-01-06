using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{

    [DataContract(Name = "AdditionalFieldsPostPaid")]
    public class AdditionalFields
    {
        [DataMember]
        public string TCXCN_CODIGO { get; set; }
        [DataMember]
        public string TCCCN_CODIGO { get; set; }
        [DataMember]
        public string TCCVV_VALOR { get; set; }

        [DataMember]
        public string TCCVN_CODIGO { get; set; }
        [DataMember]
        public string SPERN_CODIGO { get; set; }
        
        
    }
}
