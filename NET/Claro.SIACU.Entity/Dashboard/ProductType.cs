using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard
{
    [DbTable("TEMPO")]
    [DataContract(Name = "ProductType")]
    public class ProductType
    {
        [DbColumn("CODIGO_PRODUCTO")]
        [DataMember]
        public string CODIGO_PRODUCTO { get; set; }

        [DbColumn("TIPO_PRODUCTO")]
        [DataMember]
        public string TIPO_PRODUCTO { get; set; }
    }    
}
