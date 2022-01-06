using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "NumbersTriation")]
    public class NumbersTriation
    {
        [DataMember]
        public string Codigo { get; set; }

        [Data.DbColumn("x_clarolocal1")]
        [DataMember]
        public string Descripcion { get; set; }

        [Data.DbColumn("x_clarolocal2")]
        [DataMember]
        public string Descripcion2 { get; set; }

        [Data.DbColumn("x_clarolocal3")]
        [DataMember]
        public string Descripcion3 { get; set; }

        [Data.DbColumn("x_clarolocal4")]
        [DataMember]
        public string Descripcion4 { get; set; }

        [Data.DbColumn("x_clarolocal5")]
        [DataMember]
        public string Descripcion5 { get; set; }

        [Data.DbColumn("x_clarolocal6")]
        [DataMember]
        public string Descripcion6 { get; set; }

        [Data.DbColumn("x_claro_ldn1")]
        [DataMember]
        public string Descripcion7 { get; set; }

        [Data.DbColumn("x_claro_ldn2")]
        [DataMember]
        public string Descripcion8 { get; set; }

        [Data.DbColumn("x_claro_ldn3")]
        [DataMember]
        public string Descripcion9 { get; set; }

        [Data.DbColumn("x_claro_ldn4")]
        [DataMember]
        public string Descripcion10 { get; set; }

        [Data.DbColumn("x_charge_amount")]
        [DataMember]
        public string Monto { get; set; }
    }
}
