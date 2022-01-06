using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.CustomerSearch
{
    public class SearchCustomerModel
    {
        public string DocumentType { get; set; }
        public string DocumentTypeDescription { get; set; } 
        public string DocumentNumber { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string ParentAccount { get; set; }
        public string CustomerStatus { get; set; }
        public string Segment { get; set; }
        public string MigrateOne { get; set; }  
    }
}