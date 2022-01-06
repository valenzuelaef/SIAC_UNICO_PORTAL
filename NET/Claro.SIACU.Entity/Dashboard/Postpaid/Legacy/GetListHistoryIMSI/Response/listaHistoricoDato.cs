
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetListHistoryIMSI.Response
{
    public class listaHistoricoDato
    {
        [DataMember]
        public string cd_id { get; set; }
        [DataMember]
        public string cd_seqno { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string motivo { get; set; }
        [DataMember]
        public string introducido_al { get; set; }
        [DataMember]
        public string valido_desde { get; set; }
        [DataMember]
        public string introducido_por { get; set; }
        [DataMember]
        public string iccid { get; set; }
        [DataMember]
        public string modificado { get; set; }
        [DataMember]
        public string imsi { get; set; }
        [DataMember]
        public string cd_rs_id { get; set; }
    }
}
