using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.SalesCriteria
{
    public class FilterParametersModel
    {
        public string Segment { get; set; }
        public string CustomerTypeId { get; set; }
        public string OperationTypeId { get; set; }
        public string CustomerSubTypeId { get; set; }
        public string ProductTypeId { get; set; }
        public string DocumentTypeId { get; set; }
        public string MigrationStatusId { get; set; }
    }
}