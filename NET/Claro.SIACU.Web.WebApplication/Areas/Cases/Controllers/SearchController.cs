using System;
using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.Areas.Cases.Controllers
{
    public class SearchController : Controller
    {
        private readonly WipBinService.WipBinServiceClient objWipBinService = new WipBinService.WipBinServiceClient();

        public ActionResult Index()
        {
            return PartialView("Index");
        }

        [HttpPost]
        public ActionResult Start(string strIdSession, string strSearchBy, string strSearchValue)
        {
            Claro.SIACU.Web.WebApplication.WipBinService.CasesByIdRequest objCasesByIdRequest = new WipBinService.CasesByIdRequest()
            {
                Audit = App_Code.Common.CreateAuditRequest<Claro.Entity.AuditRequest>(strIdSession)                
            };

            if (strSearchBy == "byReportId")
            {
                objCasesByIdRequest.ReportId = strSearchValue;
            }
            else if (strSearchBy == "byComplaintId")
            {
                objCasesByIdRequest.ComplaintId = strSearchValue;
            }
            else
            {
                objCasesByIdRequest.CaseId = strSearchValue;
            }

            try
            {
                Claro.SIACU.Web.WebApplication.WipBinService.CasesByIdResponse objCasesByIdResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.WipBinService.CasesByIdResponse>(
                    () => { return objWipBinService.GetCasesById(objCasesByIdRequest); }
                );

                return PartialView("_SearchResult", objCasesByIdResponse);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCasesByIdRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objCasesByIdRequest.Audit.Transaction);
            }
        }
    }
}