using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSuspendedContract;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataAccount
{
    public class AccountSuspendedContractModel
    {       
        public List<AccountTypeSuspended> lstTypeSuspendedModel { get; set; }
        public List<AccountSuspendedContract> lstAccountSuspendedContract { get; set; }

        public AccountSuspendedContractModel()
        {
            lstTypeSuspendedModel = new List<AccountTypeSuspended>();
            lstAccountSuspendedContract = new List<AccountSuspendedContract>();
        }
    }
}