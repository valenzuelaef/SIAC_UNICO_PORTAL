
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService
{
    public class PortabilityModel
    {
        public PortabilityModel() { }

        public string NumberRequest { get; set; }
        public string ProcessStatus { get; set; }
        public string DescriptionProcessStatus { get; set; }
        public string RegistrationDate { get; set; }
        public string TypePortability { get; set; }
        public string ExecutionDate { get; set; }
        public string ReceivingOperatorCode { get; set; }
        public string TransferorCode { get; set; }
        public string Answer { get; set; }
        public string TypeProcessDate { get; set; }
        public string TypeProcessOperator { get; set; }
        public string Operator { get; set; }
    }
}