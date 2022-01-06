using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    public class ShareRoaming
    {
        [DataMember]
        public int CTRON_COD_APLIC { get; set; }

        [DataMember]
        public string CTROV_TIPO_CLIENTE { get; set; }

        [DataMember]
        public string CTROV_ZONA_EXC { get; set; }

        [DataMember]
        public string CTROV_ZONA_RM { get; set; }

        [DataMember]
        public string CTROV_USU_REGISTRO { get; set; }

        [DataMember]
        public string CTROV_USU_MODIFICO { get; set; }

        [DataMember]
        public string CTROD_FEC_REGISTRO { get; set; }

        [DataMember]
        public string CTROD_FEC_ACTUALIZO { get; set; }

        [DataMember]
        public string CTROV_PCRF_CE { get; set; }

        [DataMember]
        public string CTROV_PCRF { get; set; }

        [DataMember]
        public string CTROV_ACT_CUOTA { get; set; }

        [DataMember]
        public string CTROV_ACT_MASIVO { get; set; }

        [DataMember]
        public string CTROV_ACT_CORP { get; set; }

        [DataMember]
        public string CTROV_ZNEXC_CAP { get; set; }

        [DataMember]
        public string CTROV_ZNRM_CAP { get; set; }
    }
}
