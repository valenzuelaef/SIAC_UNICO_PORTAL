using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.Instant;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models
{
    public class InstantModel
    {
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy, hh.mm tt}", ApplyFormatInEditMode = true)]
        public List<Instant> listInstant { get; set; }
        public string NumberRegisters { get; set; }
        public string TextFilter { get; set; }       
    }
}