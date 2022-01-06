using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using KEY = Claro.ConfigurationManager;



namespace Claro.SIACU.Web.WebApplication.Areas.Management.Controllers
{
    public class MassiveInstantController : Controller
    {

        readonly Models.InstantModel objDataInstantModel = new Models.InstantModel();
        Helpers.Instant.DataInstant objDataInstant;

        public ActionResult Index(string strIdSession, string strAplication)
        {
            return PartialView();

        }

        public JsonResult ImportLine(string strIdSession, string strAplication, string strTypeSearch)
        {
            string DirectoryPath = null;
            string Archive = null;
            string FullPath = null;
            Models.InstantModel objInstantModel = null;

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                DirectoryPath = (Server.MapPath("~"));
                if (!Directory.Exists(DirectoryPath + @"\Temporal"))
                {
                    Directory.CreateDirectory(DirectoryPath + @"\Temporal");
                }
                Archive = file.FileName;
                Archive = Path.GetFileName(Archive);
                file.SaveAs((DirectoryPath) + @"\Temporal\" + Archive);
                FullPath = DirectoryPath + @"\Temporal\" + Archive;
                objInstantModel = UpdateFileData(strIdSession, FullPath, strAplication, strTypeSearch);
            }
            return Json(new { data = objInstantModel });
        }

        public Models.InstantModel UpdateFileData(string strIdSession, string FullPath, string Application, string strTypeSearch)
        {
            List<Helpers.Instant.DataInstant> listInstant = new List<Helpers.Instant.DataInstant>();
            var StrRd = new StreamReader(FullPath);
            string[] Lines = System.Text.RegularExpressions.Regex.Split(StrRd.ReadToEnd(), Environment.NewLine);
            string[] FiltroLines = Lines.Distinct().ToArray();
            StrRd.Close();
            objDataInstantModel.ResultFlag = false;
            if (Lines.Length > 1000)
            {
                System.IO.File.Delete(FullPath);
                objDataInstantModel.ResultFlag = true;
            }
            else
            {
                foreach (string strNumber in FiltroLines)
                {
                    if (strNumber != "")
                    {
                        objDataInstant = new Helpers.Instant.DataInstant();
                        objDataInstant.Number = strNumber;
                        DashboardService.CustomerResponseDashboard objCustomerResponseDashboard;
                        DashboardService.CustomerRequestDashboard objCustomerRequestDashboard = new DashboardService.CustomerRequestDashboard()
                        {
                            ValueSearch = strNumber,
                            ApplicationType = "",
                            TypeSearch = strTypeSearch,
                            audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession)
                        };
                        try
                        {
                            objCustomerResponseDashboard =
                            Claro.Web.Logging.ExecuteMethod<DashboardService.CustomerResponseDashboard>(
                            objCustomerRequestDashboard.audit.Session,
                            objCustomerRequestDashboard.audit.transaction,
                            () => { return new DashboardService.DashboardServiceClient().GetCustomerForInstMasive(objCustomerRequestDashboard); });
                        }
                        catch (Exception ex)
                        {
                            objCustomerResponseDashboard = null;
                        }

                        if (objCustomerResponseDashboard.InterfaceCustomer != null)
                        {
                            if (objCustomerResponseDashboard.ApplicationType == Application)
                            {
                                objDataInstant.Status = "Ok";
                            }
                            else
                            {
                                objDataInstant.Status = "Error";
                                objDataInstant.Description = "El número de la consulta, no pertenece al tipo de producto" + Application;
                            }
                        }
                        else
                        {
                            objDataInstant.Status = "Error";
                            objDataInstant.Description = "Problema con el WebServices";

                        }

                        listInstant.Add(objDataInstant);
                    }
                    objDataInstantModel.listInstant = listInstant;
                }
                System.IO.File.Delete(FullPath);
            }
            return objDataInstantModel;
        }

        public JsonResult SaveInstantMasive(string strIdSession, string strAplication, Models.InstantModel objInstantModel)
        {
            CommonService.InstantResponseManagement objInstantResponseManagement = null;
            Models.InstantModel objInstantModels = objInstantModel;
            string DateStar = string.Format("{0} {1}:{2} {3}", objInstantModels.DateValidityStart.ToString("dd/M/yyyy"), objInstantModels.StartHour, objInstantModels.StartMinutes, objInstantModels.StartType);
            string DateEnd = string.Format("{0} {1}:{2} {3}", objInstantModels.DateValidityEnd.ToString("dd/M/yyyy"), objInstantModels.EndHour, objInstantModels.EndMinutes, objInstantModels.EndType);

            List<Claro.SIACU.Web.WebApplication.CommonService.Instant> lstInstant = new List<CommonService.Instant>();
            foreach (var item in objInstantModels.listInstant)
            {
                if (item.Number != "")
                {
                    Claro.SIACU.Web.WebApplication.CommonService.Instant objInstant = new Claro.SIACU.Web.WebApplication.CommonService.Instant();
                    if (strAplication.Equals("POSTPAID") || strAplication.Equals("PREPAID"))
                    {
                        objInstant.DATO = item.Number.Trim().ToString();
                        objInstant.CUENTA = "";
                    }
                    else
                    {
                        objInstant.DATO = "";
                        objInstant.CUENTA = item.Number.Trim().ToString();
                    }
                    objInstant.TIPOTELEFONO = KEY.AppSettings("strTipoTelefonoPostPago");
                    objInstant.DESCRIPCION = objInstantModels.Description;
                    objInstant.FECHA_VIGENCIA_INICIO = Convert.ToDate(DateStar);
                    objInstant.FECHA_VIGENCIA_FIN = Convert.ToDate(DateEnd);
                    objInstant.LOGIN = Environment.UserName;
                    objInstant.COLOR = KEY.AppSettings("strColorInstMasive");
                    lstInstant.Add(objInstant);
                }
            }

            CommonService.InstantRequestManagement objInstantRequestManagement = new CommonService.InstantRequestManagement()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession),
                lstInstant = lstInstant
            };
            try
            {
                objInstantResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.InstantResponseManagement>(() => { return new CommonService.CommonServiceClient().InsertInstants(objInstantRequestManagement); });
            }
            catch (Exception ex)
            {
                objInstantResponseManagement = null;
            }


            return Json(new { data = objInstantResponseManagement });
        }

        public ActionResult MasiveInstantFail(string strIdSession)
        {
            return PartialView();
        }

        public JsonResult ImportArchive(string strIdSession, string strAplication, string strTypeSearch)
        {
            string DirectoryPath = null;
            string NewArchiveName = "";
            string FullPath = null;
            string ArchiveName = "C12640" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                DirectoryPath = (Server.MapPath("~"));
                if (!Directory.Exists(DirectoryPath + @"\Temporal"))
                {
                    Directory.CreateDirectory(DirectoryPath + @"\Temporal");
                }
                file.SaveAs((DirectoryPath) + @"\Temporal\" + ArchiveName + "_1.txt");
                FullPath = DirectoryPath + @"\Temporal\" + ArchiveName + "_1.txt";
                NewArchiveName = GetCorrelative(strIdSession, FullPath, strAplication, strTypeSearch);
            }

            List<string> objstr = new List<string>();
            objstr.Add(ArchiveName);
            objstr.Add(NewArchiveName);

            return Json(new { data = objstr });
        }

        public string GetCorrelative(string strIdSession, string FullPath, string Application, string strTypeSearch)
        {
            CommonService.InstantResponseManagement objInstantResponseManagement = null;
            string strCorrelative = "";

            StreamReader sr = new StreamReader(FullPath);
            string strTxt = sr.ReadToEnd().Replace(Environment.NewLine, "").Replace(" ", "").Trim();
            sr.Close();
            System.Text.RegularExpressions.Regex strExpression = new System.Text.RegularExpressions.Regex("^\\d+$");
            if (strExpression.IsMatch(strTxt))
            {
                CommonService.InstantRequestManagement objInstantRequestManagement = new CommonService.InstantRequestManagement()
                {
                    audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession),
                };
                objInstantResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.InstantResponseManagement>(() => { return new CommonService.CommonServiceClient().GetInstantCorrelative(objInstantRequestManagement, "C12640"); });
                if (objInstantResponseManagement.result)
                {
                    strCorrelative = "Instantaneas_" + "C12640" + "_" + objInstantResponseManagement.message + ".txt";
                }
            }

            return strCorrelative;
        }

        public JsonResult SaveProcessedOrders(string strIdSession, string strAplication, Models.InstantModel objInstantModel)
        {
            Models.InstantModel objInstantModels = objInstantModel;
            CommonService.InstantResponseManagement objInstantResponseManagement;

            string DateStar = string.Format("{0} {1}:{2} {3}", objInstantModels.DateValidityStart.ToString("dd/M/yyyy"), objInstantModels.StartHour, objInstantModels.StartMinutes, objInstantModels.StartType);
            string DateEnd = string.Format("{0} {1}:{2} {3}", objInstantModels.DateValidityEnd.ToString("dd/M/yyyy"), objInstantModels.EndHour, objInstantModels.EndMinutes, objInstantModels.EndType);
            string strUrlServer = Server.MapPath("~") + @"\Temporal\";

            StreamReader sr = new StreamReader(strUrlServer + objInstantModel.ArchiveName + "_1.txt");
            string[] arrLines = System.Text.RegularExpressions.Regex.Split(sr.ReadToEnd(), Environment.NewLine);
            string[] FiltroArrLines = arrLines.Distinct().ToArray();
            sr.Close();

            CommonService.Instant objInstant = new CommonService.Instant()
            {
                LOGIN = "C12640",
                NOMBRE_ARCHIVO_INSTANTANEA = objInstantModel.NewArchiveName,
                NOMBRE_ARCHIVO_DESCRIPCION = "DescRegistro_" + objInstantModel.NewArchiveName,
                DESCRIPCION = objInstantModel.Description,
                TIPOTELEFONO = "12",
                FECHA_VIGENCIA_INICIO = DateTime.Parse(DateStar),
                FECHA_VIGENCIA_FIN = DateTime.Parse(DateEnd)
            };

            List<CommonService.Instant> InstantList = new List<CommonService.Instant>();
            foreach (var item in FiltroArrLines)
            {
                if (item != "")
                {
                    CommonService.Instant objInstantList = new CommonService.Instant();
                    objInstantList.DATO = item.Trim();
                    InstantList.Add(objInstantList);
                }
            }

            CommonService.InstantRequestManagement objInstantRequestManagement = new CommonService.InstantRequestManagement()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession),
                instant = objInstant,
                lstInstant = InstantList
            };

            objInstantResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.InstantResponseManagement>(() => { return new CommonService.CommonServiceClient().SaveIntantsMasive(objInstantRequestManagement); });

            return Json(new { data = objInstantResponseManagement });
        }

    }
}