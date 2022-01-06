using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "HistoricDeliveryPostpaid")]
    public class HistoricDelivery
    {
        [Data.DbColumn("RECIBO")]
        [DataMember]
        public string _RECIBO { get; set; }

        [DataMember]
        public string _FECHAINI { get; set; }

        [DataMember]
        public string _FECHAFIN { get; set; }

        [Data.DbColumn("FECEMISION")]
        [DataMember]
        public string _FECEMISION { get; set; }


        [Data.DbColumn("TIPO")]
        [DataMember]
        public string _TIPO { get; set; }

        [Data.DbColumn("COURIER")]
        [DataMember]
        public string _COURIER { get; set; }

        [Data.DbColumn("ESTADO")]
        [DataMember]
        public string _ESTADO { get; set; }

        [Data.DbColumn("MOTIVO")]
        [DataMember]
        public string _MOTIVO { get; set; }

        [Data.DbColumn("FECENTREGA")]
        [DataMember]
        public string _FECENTREGA { get; set; }

        [DataMember]
        public string _ARCHIVO { get; set; }

        [DataMember]
        public string _RUTA { get; set; }

        [DataMember]
        public int _CANT_REG { get; set; }

        [DataMember]
        public string _USUARIO { get; set; }

        [DataMember]
        public string _FECHASOL { get; set; }

        [DataMember]
        public string _FECHAEJE { get; set; }

        [DataMember]
        public string _FLAG { get; set; }
    }
}