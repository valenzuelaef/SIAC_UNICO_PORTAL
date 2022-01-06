﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.SalesCriteria
{
    public class DocumentType
    {
        [JsonProperty("Code")]
        public string Code { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("EstadoMigracion")]
        public List<MigrationStatus> MigrationStatus { get; set; }
    }
}