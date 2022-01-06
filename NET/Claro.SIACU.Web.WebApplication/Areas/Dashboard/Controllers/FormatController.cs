using System.Web.Mvc;


namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class FormatController : Controller
    {
        public ActionResult AccountPinPukConstancy(string strIdSession,string srtAccount, string strBusinessName)
        {          
            return View("Postpaid/AccountPinPukConstancy", new PostpaidDataAccountController().GetPinPuk(strIdSession,srtAccount, strBusinessName));
        }
        public ActionResult PrintCall(string strIdSession, string strTelephoneCustomer, string strDateFrom, string strDateTo)
        {
            return View("Prepaid/PrintCall", new PrepaidDataCallController().getPrintCall(strIdSession, strTelephoneCustomer, strDateFrom, strDateTo));            
        }
	}
}