using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Coliving.getListOST
{
    [DataContract]
    [Data.DbTable("TEMPO")]
    public class TechnicalServicesOSTType
    {
        [DataMember]
        [Data.DbColumn("ID_OST")]
        public String idOst { get; set; }
        [DataMember]
        [Data.DbColumn("CAC_DESC")]
        public String Cac { get; set; }
        [DataMember]
        [Data.DbColumn("GEN_FECHA")]
        public String FechaGeneracion { get; set; }
        [DataMember]
        [Data.DbColumn("EQU_IMEI")]
        public String Imei { get; set; }
        [DataMember]
        [Data.DbColumn("MARCA")]
        public String Marca { get; set; }
        [DataMember]
        [Data.DbColumn("MODELO")]
        public String Modelo { get; set; }


    }
}
