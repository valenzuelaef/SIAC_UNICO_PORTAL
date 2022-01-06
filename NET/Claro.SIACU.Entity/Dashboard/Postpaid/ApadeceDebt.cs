using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ApadeceDebtPostPaid")]
    public class ApadeceDebt
    {
        [DataMember]
        public string DOCUMENTO_CLIENTE { get; set; }


        [Data.DbColumn("custcode")]
        [DataMember]
        public string CODIGO_CLIENTE { get; set; }


        [Data.DbColumn("CCFNAME")]
        [DataMember]
        public string NOMBRES_CLIENTE { get; set; }


        [Data.DbColumn("cclname")]
        [DataMember]
        public string APELLIDOS_CLIENTE { get; set; }


        [Data.DbColumn("ccname")]
        [DataMember]
        public string REPRESENTANTE_CLIENTE { get; set; }

        [DataMember]
        public string ESTADO_CLIENTE { get; set; }


        [Data.DbColumn("prgname")]
        [DataMember]
        public string TIPO_CLIENTE { get; set; }


        [Data.DbColumn("deuda")]
        [DataMember]
        public string DEUDA_CLIENTE { get; set; }

    }
}
