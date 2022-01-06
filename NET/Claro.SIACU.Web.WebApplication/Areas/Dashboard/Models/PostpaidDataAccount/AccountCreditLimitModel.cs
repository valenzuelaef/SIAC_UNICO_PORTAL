using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountCreditLimit;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataAccount
{
    public class AccountCreditLimitModel
    {
       
        public string Account { get; set; }
        public string BusinessName { get; set; }
        public List<AccountCreditLimit> lstAccountCreditLimit { get; set; }
    }
}