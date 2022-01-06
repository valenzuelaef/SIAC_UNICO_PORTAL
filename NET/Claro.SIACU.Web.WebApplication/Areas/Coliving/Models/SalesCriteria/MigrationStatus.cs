using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.SalesCriteria
{
    public class MigrationStatus
    {
        [JsonProperty("Code")]
        public string Code { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("CodePage")]
        public string CodePage { get; set; }
        [JsonProperty("Page")]
        public string Page { get; set; }
        public string Url { get; set; }
    }
}