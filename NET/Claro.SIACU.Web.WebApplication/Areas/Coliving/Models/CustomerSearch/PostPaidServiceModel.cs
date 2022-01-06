using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.CustomerSearch
{
    public class PostPaidServiceModel
    {
        public string BillingAccountId { get; set; }
        public string MigrateOne { get; set; }
        public string ProductType { get; set; }
        public string CustomerType { get; set; }    
        public string CountActivateLine { get; set; }
        public string CountInactiveLine { get; set; }
        public string OrigenInfoPost { get; set; } 
        public List<AccountSubscription> SubscriptionActive { get; set; }
        public List<AccountSubscription> SubscriptionInactive { get; set; }     
    }
}