using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.CustomerSearch
{
    public class AccountCustomerModel : SearchCustomerModel
    {
        public string ProductType { get; set; }
        public List<AccountSubscription> ListSuscription { get; set; }
    }
}