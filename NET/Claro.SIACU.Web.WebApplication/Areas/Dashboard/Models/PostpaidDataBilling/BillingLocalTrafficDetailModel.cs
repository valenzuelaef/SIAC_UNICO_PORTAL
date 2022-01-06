using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BillingLocalTrafficDetail;
using System.Collections.Generic;
using HELPER_DATA = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LocalTrafficDetail;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataBilling
{
    public class BillingLocalTrafficDetailModel
    {

        public List<HELPER_DATA.LocalTrafficDetail> listTimProLocalTrafficDetail { get; set; }
        public List<HELPER_DATA.LocalTrafficDetail> listTimMaxLocalTrafficDetail { get; set; }
        public List<HELPER_DATA.LocalTrafficDetailCallTP> listLocalTrafficDetailCallTP { get; set; }
        public List<HELPER_DATA.LocalTrafficDetailCallTM> listLocalTrafficDetailCallTM { get; set; }
        public List<HELPER_DATA.LocalTrafficDetailCallTP> listConsumeLocalTrafficDetailCallTP { get; set; }
        public List<HELPER_DATA.LocalTrafficDetailCallTM> listConsumeLocalTrafficDetailCallTM { get; set; }
        public LocalTrafficDetailConstants objLocalTrafficDetailConstants { get; set; }
        public GeneralLocalTrafficDetailConstants objGeneralLocalTrafficDetailConstants { get; set; }

        public string strLocalTrafficType { get; set; }
        public string strInvoiceNumber { get; set; }
        public string LocalTrafficQuantityCallTP { get; set; }
        public string LocalTrafficQuantityCallTM { get; set; }
        public string ConsumeLocalTrafficQuantityCallTP { get; set; }
        public string ConsumeLocalTrafficQuantityCallTM { get; set; }
    }
}