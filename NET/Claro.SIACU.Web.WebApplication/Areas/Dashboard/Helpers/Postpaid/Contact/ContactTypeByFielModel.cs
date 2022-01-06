using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact
{
    public class ContactTypeByFielModel
    {
        public int TCXCN_CODIGO { get; set; }
        public int TCCCN_CODIGO { get; set; }
        public string TCXCC_OBLIGATORIO { get; set; }
        public string TCCCC_EDITABLE { get; set; }
        public int TCCCN_ORDEN { get; set; }

        public List<ContactTypeByFielModel> lstContactTypeByFiel { get; set; }

        public ContactTypeByFielModel()
        {
            lstContactTypeByFiel = new List<ContactTypeByFielModel>();
        }
    }
}