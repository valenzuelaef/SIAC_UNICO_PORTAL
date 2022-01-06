using System;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Web.WebApplication.Areas.Cases.Controllers
{
    public class QueueController : Controller
    {
        private readonly WipBinService.WipBinServiceClient objWipBinService = new WipBinService.WipBinServiceClient();

        [HttpPost]
        public ActionResult Index(string strIdSession)
        {
            string strFlagWorkGroup = KEY.AppSettings("strFlagWorkGroup");

            Claro.SIACU.Web.WebApplication.WipBinService.QueuesRequest objQueuesRequest = new WipBinService.QueuesRequest()
            {
                Audit = App_Code.Common.CreateAuditRequest<Claro.Entity.AuditRequest>(strIdSession),
                FlagWorkGroup = strFlagWorkGroup,
            };

            Claro.SIACU.Web.WebApplication.WipBinService.QueuesResponse objQueuesResponse = null;

            try
            {
                objQueuesResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.WipBinService.QueuesResponse>(
                    () => { return objWipBinService.GetQueues(objQueuesRequest); }
                );

                return PartialView("Index", objQueuesResponse);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objQueuesRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objQueuesRequest.Audit.Transaction);
            }
        }

        [HttpPost]
        public ActionResult QueueDetail(string strIdSession, string strQueueName)
        {
            Claro.SIACU.Web.WebApplication.WipBinService.CasesByQueueUserRequest objCasesByQueueUserRequest = new WipBinService.CasesByQueueUserRequest()
            {
                Audit = App_Code.Common.CreateAuditRequest<Claro.Entity.AuditRequest>(strIdSession),
                Queue = strQueueName,
            };

            try
            {
                Claro.SIACU.Web.WebApplication.WipBinService.CasesByQueueUserResponse objCasesByQueueUserResponse = null;
                objCasesByQueueUserResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.WipBinService.CasesByQueueUserResponse>(
                    () => { return objWipBinService.GetCasesByQueueUser(objCasesByQueueUserRequest); }
                );

                ViewBag.Queue = strQueueName;
                return PartialView("_QueueDetail", objCasesByQueueUserResponse);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCasesByQueueUserRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objCasesByQueueUserRequest.Audit.Transaction);
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