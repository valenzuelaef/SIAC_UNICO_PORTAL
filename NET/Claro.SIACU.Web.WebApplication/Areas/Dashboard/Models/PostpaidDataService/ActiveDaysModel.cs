
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class ActiveDaysModel
    {
        public int ActiveDays { get; set; }
        public int DisableDays { get; set; }
        public bool StateMessageError { get; set; }
        public string MessageError { get; set; }
        public string Message { get; set; }

    }
}