
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.ServicePostpaid
{
    public class ServicePostpaid
    {
        public int Id { get; set; }
        public string IdPlan { get; set; }
        public string TypeProduct { get; set; }

        public string CodProduct { get; set; }
        public string Product { get; set; }
        public string Account { get; set; }
        public string CodClient { get; set; }
        public string TypeClient { get; set; }
        public string DateAccountActivation { get; set; }
        public int NumberServicesActive { get; set; }
        public int NumberServicesNotActive { get; set; } 
    }
}