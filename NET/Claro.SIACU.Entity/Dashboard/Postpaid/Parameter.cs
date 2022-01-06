using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ParameterPostPaid")]
    [Data.DbTable("TEMPO")]
    public class Parameter
    {
        [DataMember]
        [Data.DbColumn("PARAMETRO_ID")]
        public decimal PARAMETRO_ID { get; set; }

        [DataMember]
        [Data.DbColumn("NOMBRE")]
        public string NOMBRE { get; set; }

        [DataMember]
        [Data.DbColumn("DESCRIPCION")]
        public string DESCRIPCION { get; set; }

        [DataMember]
        [Data.DbColumn("TIPO")]
        public string TIPO { get; set; }

        [DataMember]
        [Data.DbColumn("VALOR_C")]
        public string VALOR_C { get; set; }

        [DataMember]
        [Data.DbColumn("VALOR_N")]
        public decimal VALOR_N { get; set; }

        [DataMember]
        [Data.DbColumn("VALOR_L")]
        public string VALOR_L { get; set; }
        [DataMember]
        public string VALOR { get; set; }
        [DataMember]
        public string VALOR2 { get; set; }
    }
}