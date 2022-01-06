using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.Areas.Coliving
{
    public class ColivingAreaRegistration:AreaRegistration
    {

        public override string AreaName
        {
            get
            {
                return "Coliving";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Coliving_default",
                "Coliving/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}