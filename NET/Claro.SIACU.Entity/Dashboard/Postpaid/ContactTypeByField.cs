using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ContactTypeByFieldPostPaid")]
    public  class ContactTypeByField
    {
        [DataMember]
        [Data.DbColumn("TCXCN_CODIGO")]
        public int TCXCN_CODIGO { get; set; }
       
        [DataMember]
        [Data.DbColumn("TCCCN_CODIGO")]
        public int TCCCN_CODIGO { get; set; }
        
        [DataMember]
        [Data.DbColumn("TCXCC_OBLIGATORIO")]
        public string TCXCC_OBLIGATORIO { get; set; }
       
        [DataMember]
        [Data.DbColumn("TCCCC_EDITABLE")]
        public string TCCCC_EDITABLE { get; set; }
        
        [DataMember]
        [Data.DbColumn("TCCCN_ORDEN")]
        public int TCCCN_ORDEN { get; set; }

       
    }
}
