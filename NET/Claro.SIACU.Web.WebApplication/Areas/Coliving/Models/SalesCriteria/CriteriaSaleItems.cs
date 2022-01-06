using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Newtonsoft.Json;
namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.SalesCriteria
{
    public class CriteriaSaleItems
    {
        [JsonProperty("listItems")]
        public List<Modality> ListModality { get; set; }
    }
}