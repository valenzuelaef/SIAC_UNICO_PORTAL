using Claro.Helpers;
using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataAccount
{
    public class AccountSharedBagModel: IExcel
    {
        public string Account { get; set; }
        public string Line { get; set; }
        [Header(Title = "lstSharedBag")]
        public List<AccountSharedBag> lstSharedBag { get; set; }
        [Header(Title = "lstSharedBagLine")]
        public List<AccountSharedBagLine> lstSharedBagLine { get; set; }
        public string Error { get; set; }
        public bool isData { get; set; }
        public string MessageTypeCustomer { get; set; }
        public string CountSharedBag { get; set; }

    }
}