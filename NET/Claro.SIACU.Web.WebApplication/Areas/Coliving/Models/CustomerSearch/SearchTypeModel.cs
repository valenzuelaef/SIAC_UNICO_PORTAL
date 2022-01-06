using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.CustomerSearch
{
    public class SearchTypeModel
    {
        public String LineNumber { get; set; }
        public String CustomerId { get; set; }
        public String DocumentType { get; set; }
        public String DocumentNumber { get; set; }
        public String SearchType { get; set; }
        public String DocumentTypeDescription { get; set; }
    }
}