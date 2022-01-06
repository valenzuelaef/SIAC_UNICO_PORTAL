using System.Collections.Generic;
using HELPER_DATA = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LongDistance;


namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataBilling
{
    public class BillingDetailLongDistanceModel
    {
        public List<HELPER_DATA.CallDetail> listCallDetail { get; set; }
        public List<HELPER_DATA.CallDetailSMS> listCallDetailSMS { get; set; }
        public List<HELPER_DATA.CallDetailTimService> listCallDetailTimService { get; set; }
        public string TypeApplication { get; set; }
        public string DescriptionTypeCall { get; set; }
        public decimal decCantidadSMS { get; set; }
    }

    
   
    
}