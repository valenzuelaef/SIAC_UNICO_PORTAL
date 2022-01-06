
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact
{
    public class ContactTypeModel
    {
        public int TCCTN_CODIGO { get; set; }
        public int TCCTN_ORDEN { get; set; }
        public string TCCTV_NOMBRE { get; set; }
        public string TCCTV_SISACT_SIAC_DES { get; set; }
        public string TCCTV_OBLIGATORIO_DES { get; set; }
        public string TCCT_ESTADO_DES { get; set; }
        public bool TCCTC_ESTADO { get; set; }
        public bool TCCTC_COPIABLE { get; set; }
        public bool TCCTC_OBLIGATORIO { get; set; }
        public int TCCTI_MINREGISTROS { get; set; }
        public int TCCTI_MAXREGISTROS { get; set; }
        public bool TCCTC_VISIBLESIACPOST { get; set; }
        public bool TCCTC_VISIBLESISACT { get; set; }
    }
}