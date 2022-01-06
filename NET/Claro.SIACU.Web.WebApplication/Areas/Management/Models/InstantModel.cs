using System;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Management.Models
{
    public class InstantModel
    {
        public List<Helpers.Instant.DataInstant> listInstant { get; set; }
        public string NumberRegisters { get; set; }
        public string TextFilter { get; set; }
        public string option { get; set; }
        public string StartHour { get; set; }
        public string StartMinutes { get; set; }
        public string StartType { get; set; }
        public string EndHour { get; set; }
        public string EndMinutes { get; set; }
        public string EndType { get; set; }
        public Helpers.Instant.DataInstant objInstant { get; set; }
        public string MessageResult { get; set; }
        public bool ResultFlag { get; set; }
        public string OriginType { get; set; }
        public DateTime DateValidityStart { get; set; }
        public DateTime DateValidityEnd { get; set; }
        public string Description { get; set; }
        public string ArchiveName { get; set; }
        public string NewArchiveName { get; set; }
    }
}