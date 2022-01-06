using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.CustomerSearch
{
    public class LineCustomerModel : SearchCustomerModel
    {
        public string ServiceIdentifier { get; set; }
        public string ProductType { get; set; }
        public string PoName { get; set; }
        public string SubscriptionStatus { get; set; }
    }
}