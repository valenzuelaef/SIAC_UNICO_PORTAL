using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard
{
    public class DashboardAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return Claro.SIACU.Constants.Dashboard;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                Claro.SIACU.Constants.Dashboarddefault,
                "Dashboard/{controller}/{action}/{id}",
                new { action = Claro.SIACU.Constants.Index, id = UrlParameter.Optional }
            );
        }
    }
}