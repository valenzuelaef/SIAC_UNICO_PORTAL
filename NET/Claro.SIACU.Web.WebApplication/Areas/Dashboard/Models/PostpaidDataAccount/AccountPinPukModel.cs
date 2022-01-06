using Claro.Helpers;
using System.Collections.Generic;
using PINPUK_MODELHELPERS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountPinPukHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataAccount
{
    public class AccountPinPukModel : IExcel
    {
        [Header(Title = "Account")]
        public string Account { get; set; }
        [Header(Title = "BusinessName")]
        public string BusinessName { get; set; }
        [Header(Title = "Date")]
        public string Date { get; set; }
        [Header(Title = "objPinPuk")]
        public PINPUK_MODELHELPERS.AccountPinPuk objPinPuk { get; set; }
        [Header(Title = "lstAccountPinPuk")]
        public List<PINPUK_MODELHELPERS.AccountPinPuk> lstAccountPinPuk { get; set; }

        public AccountPinPukModel()
        {
            objPinPuk = new PINPUK_MODELHELPERS.AccountPinPuk();
            lstAccountPinPuk = new List<PINPUK_MODELHELPERS.AccountPinPuk>();
        }
    }
}