using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "HistoryActionsPostPaid")]
    public class HistoryActions
    {
        [DataMember]
        public int CONTRATO { get; set; }
        [DataMember]
        public string DESCRIPCION { get; set; }
        [DataMember]
        public string SERVICIO { get; set; }
        [DataMember]
        public string FECHA { get; set; }
        [DataMember]
        public string HORA { get; set; }
        [DataMember]
        public string USUARIO { get; set; }
        [DataMember]
        public DateTime FECH_ORDE { get; set; }
        [DataMember]
        public int NRO { get; set; }

    }
}