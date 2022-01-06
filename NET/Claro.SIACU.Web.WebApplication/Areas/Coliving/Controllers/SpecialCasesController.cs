using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.SpecialCases;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Web.WebApplication.ColivingService;
namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Controllers
{
    public class SpecialCasesController : Controller
    {
        ColivingServiceClient oColivingService = new ColivingServiceClient();

        #region Views
        public ActionResult SIACU_CabeceraCasosEspeciales(CustomerModel objCustomer)
        {
            return PartialView();
        }
        #endregion
    }
}
