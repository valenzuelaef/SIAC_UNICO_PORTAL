using System;
using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.Areas.Cases.Controllers
{
    public class WipBinController : Controller
    {
        private readonly WipBinService.WipBinServiceClient objWipBinService = new WipBinService.WipBinServiceClient();

        [HttpPost]
        public ActionResult Index(string strIdSession)
        {
            Claro.SIACU.Web.WebApplication.WipBinService.WipBinListByUserRequest objWipBinListByUserRequest = new WipBinService.WipBinListByUserRequest()
            {
                Audit = App_Code.Common.CreateAuditRequest<Claro.Entity.AuditRequest>(strIdSession)
            };

            try
            {
                Claro.SIACU.Web.WebApplication.WipBinService.WipBinListByUserResponse objWipBinListByUserResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.WipBinService.WipBinListByUserResponse>(
                    () => { return objWipBinService.GetWipBinListByUser(objWipBinListByUserRequest); }
                );

                return PartialView("Index", objWipBinListByUserResponse);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objWipBinListByUserRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objWipBinListByUserRequest.Audit.Transaction);
            }
        }

        [HttpPost]
        public ActionResult BinDetail(string strIdSession, string strInboxName)
        {
            Claro.SIACU.Web.WebApplication.WipBinService.CasesByWipBinRequest objCasesByWipBinRequest = new WipBinService.CasesByWipBinRequest()
            {
                Audit = App_Code.Common.CreateAuditRequest<Claro.Entity.AuditRequest>(strIdSession),
                WipBinName = strInboxName,
            };

            Claro.SIACU.Web.WebApplication.WipBinService.CasesByWipBinResponse objCasesByWipBinResponse = null;

            try
            {
                objCasesByWipBinResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.WipBinService.CasesByWipBinResponse>(
                    () => { return objWipBinService.GetCasesByWipBin(objCasesByWipBinRequest); }
                );

                ViewBag.WipBin = strInboxName;
                return PartialView("_BinDetail", objCasesByWipBinResponse);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCasesByWipBinRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objCasesByWipBinRequest.Audit.Transaction);
            }
        }

        [HttpPost]
        public ActionResult CaseNotes(string strIdSession, string strCaseId)
        {
            Claro.SIACU.Web.WebApplication.WipBinService.CaseNotesRequest objCaseNotesRequest = new WipBinService.CaseNotesRequest()
            {
                Audit = App_Code.Common.CreateAuditRequest<Claro.Entity.AuditRequest>(strIdSession),
                CaseId = strCaseId,
            };

            try
            {
                Claro.SIACU.Web.WebApplication.WipBinService.CaseNotesResponse objCaseNotesResponse = null;
                objCaseNotesResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.WipBinService.CaseNotesResponse>(
                    () => { return objWipBinService.GetCaseNotes(objCaseNotesRequest); }
                );

                return PartialView("_Notes", objCaseNotesResponse);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCaseNotesRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objCaseNotesRequest.Audit.Transaction);
            }
        }
    }
}