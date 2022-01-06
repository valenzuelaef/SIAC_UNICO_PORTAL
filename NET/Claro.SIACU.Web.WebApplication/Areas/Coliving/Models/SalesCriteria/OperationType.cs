using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.SalesCriteria
{
    public class OperationType
    {
        [JsonProperty("Code")]
        public string Code { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Status")]
        public bool Status { get; set; }
        [JsonProperty("SubTipoCliente")]
        public List<CustomerSubType> ListCustomerSubType { get; set; }
    }
}