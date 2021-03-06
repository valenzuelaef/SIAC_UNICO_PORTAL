using System;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.Instant
{
    public class Instant
    {
        public int IdInstant { get; set; }
        public string Data { get; set; }
        public string Account { get; set; }
        public string TypePhone { get; set; }
        public string Description { get; set; }
        public DateTime DateValidityStart { get; set; }
        public DateTime DateValidityEnd { get; set; }
        public string Validity { get; set; }
        public string Login { get; set; }
        public string NameFileInstant { get; set; }
        public string NameFileDescription { get; set; }
        public DateTime DateRegister { get; set; }
        public int Number { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public string LoginRegister { get; set; }
        public string LoginModification { get; set; }
        public string DateValidityStartFormat { get; set; }
        public string DateValidityEndFormat { get; set; }
    }
}