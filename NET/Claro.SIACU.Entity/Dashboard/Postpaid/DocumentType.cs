using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "DocumentTypePostPaid")]
    public  class DocumentType
    {
        [DataMember]
        [Data.DbColumn("TDOCC_CODIGO")]
        public string  Codigo { get; set; }
        [DataMember]
        [Data.DbColumn("TDOCV_DESCRIPCION")]
        public string  Descripcion { get; set; }
        

    }
}
