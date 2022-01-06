using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace Claro.SIACU.Web.WebApplication.Support
{
    public partial class ShowFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strFilePath = Request.QueryString["strFilePath"].Trim().Replace("|", "\\");
            string strFileName = Request.QueryString["strFileName"].Trim().Replace("|", "\\");
            string strEmissionDate = Request.QueryString["strEmissionDate"].Trim();
            string strNameForm = Request.QueryString["strNameForm"].Trim();
            string strIdSession = Request.QueryString["strIdSession"].Trim();

            string strExtension = "";
            byte[] arrBuffer = null;
            string strTypeMIME = "";

            DashboardService.FileInvoiceResponseDashboard objFileInvoiceResponseDashboard = null;
            DashboardService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseDashboard = null;
            FileService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseFile = null;

            DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
            FileService.AuditRequest objAuditFileService = App_Code.Common.CreateAuditRequest<FileService.AuditRequest>(strIdSession);

            try
            {
                strExtension = System.IO.Path.GetExtension(strFilePath + strFileName).Remove(0, 1);
                DashboardService.GetTypeMIMERequestDashboard objGetTypeMIMERequestDashboard = new DashboardService.GetTypeMIMERequestDashboard()
                {
                    audit = objAudit,
                    Extension = strExtension
                };
                DashboardService.GetTypeMIMEResponseDashboard objGetTypeMIMEResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.GetTypeMIMEResponseDashboard>(() => { return new DashboardService.DashboardServiceClient().GetTypeMIME(objGetTypeMIMERequestDashboard); });
                strTypeMIME = objGetTypeMIMEResponseDashboard.TypeMime;

                if (strNameForm == Claro.ConfigurationManager.AppSettings("strTransAjusteDA"))
                {
                    DashboardService.FileInvoiceRequestDashboard objDebtDetailRequest = new DashboardService.FileInvoiceRequestDashboard()
                    {
                        Path = strFilePath + strFileName,
                        audit = objAudit
                    };
                    objFileInvoiceResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.FileInvoiceResponseDashboard>(() => { return new DashboardService.DashboardServiceClient().GetFileInvoice(objDebtDetailRequest); });
                }
                else if (strNameForm == Claro.ConfigurationManager.AppSettings("strTransAjusteTP"))
                {
                    DashboardService.FileDefaultImpersonationRequestDashboard objFileDefaultImpersonationRequestDashboard = new DashboardService.FileDefaultImpersonationRequestDashboard()
                    {
                        Path = strFileName,
                        DateRegister = strEmissionDate,
                        audit = objAudit
                    };

                    objFileDefaultImpersonationResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.FileDefaultImpersonationResponseDashboard>(1, () => { return new DashboardService.DashboardServiceClient().GetFileAjuste(objFileDefaultImpersonationRequestDashboard); });
                }
                else
                {
                    FileService.FileDefaultImpersonationRequestDashboard objFileDefaultImpersonationRequestDashboard = new FileService.FileDefaultImpersonationRequestDashboard()
                    {
                        Path = strFilePath + strFileName,
                        audit = objAuditFileService
                    };

                    objFileDefaultImpersonationResponseFile = Claro.Web.Logging.ExecuteMethod<FileService.FileDefaultImpersonationResponseDashboard>(1, () => { return new FileService.FileServiceClient().GetFileDefaultImpersonation(objFileDefaultImpersonationRequestDashboard); });
                }

                if (objFileDefaultImpersonationResponseDashboard != null)
                {
                    arrBuffer = objFileDefaultImpersonationResponseDashboard.ObjGlobalDocument.Documento;
                }
                else
                {
                    arrBuffer = objFileDefaultImpersonationResponseFile.ObjGlobalDocument.Documento;
                }

                Response.ContentType = strTypeMIME;
                Response.Clear();
                Response.BinaryWrite(arrBuffer);

                try
                {
                    Response.Flush();
                    //Response.End()
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //log.InfoFormat("Finaliza Renderiza archivo: {0}", sRutaCompletaArchivo & sNombreArchivo)
                }
                catch (ThreadAbortException ex)
                {
                    //log.Error(ex.Message)
                }
                catch (Exception ex)
                {
                    //log.Error(ex.Message)
                }

                Thread.Sleep(5000);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
