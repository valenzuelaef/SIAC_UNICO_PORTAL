using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Coliving.Common
{
    [DataContract]
    public class ColivingParameters
    {
        [DbColumn("PARAMETRO_ID")]
        [DataMember]
        public int Parameter_Id { get; set; }
        [DbColumn("NOMBRE")]
        [DataMember]
        public string Name { get; set; }
        [DbColumn("DESCRIPCION")]
        [DataMember]
        public string Description { get; set; }
        [DbColumn("VALOR_C")]
        [DataMember]
        public string Value_C { get; set; }
        [DbColumn("VALOR_N")]
        [DataMember]
        public int Value_N { get; set; }
        [DbColumn("PERIODO")]
        [DataMember]
        public int Period { get; set; }
        [DbColumn("ESTADO")]
        [DataMember]
        public string Status { get; set; }
    }
}
