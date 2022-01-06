using System;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact
{
    public class Contact
    {
        public int CSCTN_CODIGO { get; set; }
        public int SPERN_CODIGO { get; set; }
        public int SOLIN_CODIGO { get; set; }
        public int TCCTN_CODIGO { get; set; }
        public string SPERV_ESTADO { get; set; }
        public string SPERV_USU_CREA { get; set; }
        public DateTime SPERD_FEC_REG { get; set; }
        public string SPERV_NOMBRE { get; set; }
        public string SPERV_APEPATERNO { get; set; }
        public string SPERV_APEMATERNO { get; set; }
        public string SPERV_CARGO { get; set; }
        public string TDOCC_CODIGO { get; set; }
        public string SPERV_NUM_DOC { get; set; }
        public string SPERV_TEL1 { get; set; }
        public char SPERC_TIPO { get; set; }
        public string TDOCV_DESCRIPCION { get; set; }
        public int P_CUSTOMER_ID { get; set; }
        public string P_CUSTCODE { get; set; }
        public int P_SOLIN_CODIGO { get; set; }
        public int P_CSCTN_CODIGO { get; set; }

        public List<AdditionalFields> CamposAdicionales { get; set; }

        public Contact()
        {
            CamposAdicionales = new List<AdditionalFields>();
        }
    }
}