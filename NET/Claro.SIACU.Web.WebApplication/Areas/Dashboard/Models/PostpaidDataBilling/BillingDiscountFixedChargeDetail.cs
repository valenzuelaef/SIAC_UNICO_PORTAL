using System.Collections.Generic;
using HELPER_DATA = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.FixedChargeHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataBilling
{
    public class BillingDiscountFixedChargeDetailModel
    {
         public List<HELPER_DATA.DiscountFixedChargeDetail> listDiscountFixedChargeDetail { get; set; }
    }
    
}