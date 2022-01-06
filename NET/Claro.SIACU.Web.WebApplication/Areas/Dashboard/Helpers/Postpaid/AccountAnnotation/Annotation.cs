using System;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountAnnotation
{
    public class Annotation
    {
        public string Code { get; set; }
        public string CodeClient { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public string UserTracing { get; set; }
        public DateTime? DateTracing { get; set; }
        public string Date { get; set; }
        public string SDate { get; set; }
        public string ActionTracing { get; set; }
        public string DateOpening { get; set; }
        public string DateClosing { get; set; }
        public string DateModify { get; set; }
        public string NumberTickler { get; set; }
        public string Type { get; set; }
        public string DescriptionType { get; set; }
        public string StatusAction { get; set; }
    }
}