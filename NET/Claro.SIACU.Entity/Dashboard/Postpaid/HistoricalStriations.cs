using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "HistoricalStriationsPostPaid")]
    public class HistoricalStriations
    {

        [Data.DbColumn("CLIENTE")]
        [DataMember]
        public string CLIENTE { get; set; }


        [Data.DbColumn("CUENTA")]
        [DataMember]
        public string CUENTA { get; set; }


        [Data.DbColumn("NOMBRE")]
        [DataMember]
        public string NOMBRE { get; set; }


        [Data.DbColumn("AREA_ACTIVACION")]
        [DataMember]
        public string AREA_ACTIVACION { get; set; }


        [Data.DbColumn("CONTRATO")]
        [DataMember]
        public string CONTRATO { get; set; }


        [Data.DbColumn("PLAN")]
        [DataMember]
        public string PLAN { get; set; }


        [Data.DbColumn("MSISDN")]
        [DataMember]
        public string NUMERO_TELEFONO { get; set; }


        [Data.DbColumn("USUARIO")]
        [DataMember]
        public string USUARIO { get; set; }


        [Data.DbColumn("FECHA")]
        [DataMember]
        public string FECHA { get; set; }


        [Data.DbColumn("ORIGEN")]
        [DataMember]
        public string ORIGEN { get; set; }


        [Data.DbColumn("DESTINO")]
        [DataMember]
        public string DESTINO { get; set; }


        [Data.DbColumn("descripcion")]
        [DataMember]
        public string DESCRIPCION { get; set; }


        [Data.DbColumn("ESTADO")]
        [DataMember]
        public string ESTADO { get; set; }


        [Data.DbColumn("MOTIVO")]
        [DataMember]
        public string MOTIVO { get; set; }


        [Data.DbColumn("scalefactor")]
        [DataMember]
        public string FACTOR { get; set; }


        [Data.DbColumn("MODIFICACION")]
        [DataMember]
        public string MODIFICACION { get; set; }
    }
}
