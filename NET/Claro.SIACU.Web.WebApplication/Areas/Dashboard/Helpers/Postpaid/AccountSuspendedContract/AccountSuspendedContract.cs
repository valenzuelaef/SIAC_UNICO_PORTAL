using System;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSuspendedContract
{
    public class AccountSuspendedContract
    {
        public Int64 Number { get; set; }
        public string ClienteId { get; set; }
        public string CustomerType { get; set; }
        public string BusinessName { get; set; }
        public string RatePlan { get; set; }
        public string ValueShared { get; set; }
        public string SimCard { get; set; }
        public string DateActivation { get; set; }
        public string DateSuspended { get; set; }
        public string ReasonSuspended { get; set; }
        public string User { get; set; }
        public string Telephone { get; set; }
        public Boolean StateUmbral { get; set; }
    }
}