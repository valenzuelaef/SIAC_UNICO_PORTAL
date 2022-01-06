using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.Areas.Management
{
    public class ManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return Claro.SIACU.Constants.Management;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                Claro.SIACU.Constants.Managementdefault,
                "Management/{controller}/{action}/{id}",
                new { action = Claro.SIACU.Constants.Index, id = UrlParameter.Optional }
            );
        }
    }
}