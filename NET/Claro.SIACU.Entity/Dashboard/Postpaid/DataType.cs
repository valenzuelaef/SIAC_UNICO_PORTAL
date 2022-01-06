using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "DataTypePostPaid")]
    public  class DataType
    {
        [DataMember]
        [Data.DbColumn("PARAN_CODIGO")]
        public string Codigo { get; set; }
        [DataMember]
        [Data.DbColumn("PARAV_DESCRIPCION")]
        public string Descripcion { get; set; }
        
    }
}
