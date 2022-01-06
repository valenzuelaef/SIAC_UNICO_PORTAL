using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService
{
    public class DebtSaleDuesModel
    {
        public string Telephone { get; set; }
        public string Headline { get; set; }
        public string Model { get; set; }
        public string IMEI { get; set; }
        public decimal Total { get; set; }


        public string KUNNR { get; set; }        
        public decimal Mwsbk { get; set; }                
        public decimal Netwr { get; set; }        
        public string Stcd1 { get; set; }        

        public List<PendingSaleDues> listPendingSaleDues { get; set; }
        public List<CanceledSaleDues> listCanceledSaleDues { get; set; }

        public DebtSaleDuesModel()
        {
            listPendingSaleDues = new List<PendingSaleDues>();
            listCanceledSaleDues = new List<CanceledSaleDues>();
        }
    }
}