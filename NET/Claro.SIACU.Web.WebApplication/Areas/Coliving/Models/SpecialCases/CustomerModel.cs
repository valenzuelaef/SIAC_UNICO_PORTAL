using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.SpecialCases
{
    public class CustomerModel
    {
        public String LineNumber { get; set; }
        public String AccountNumber { get; set; }
        public String DocumentTypeDescription { get; set; }
        public String DocumentTypeCode { get; set; }
        public String DocumentNumber { get; set; }
        public String MigrateStatus { get; set; }
    }
}