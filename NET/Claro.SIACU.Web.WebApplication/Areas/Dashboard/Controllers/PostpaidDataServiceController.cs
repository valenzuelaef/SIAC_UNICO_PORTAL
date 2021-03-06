
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using HELPER = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid;
using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService;
using System.Linq;
using Claro.SIACU.Web.WebApplication.PostpaidService;


namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class PostpaidDataServiceController : Controller
    {
        private readonly DashboardService.DashboardServiceClient oService = new DashboardService.DashboardServiceClient();
        private readonly PostpaidService.PostpaidServiceClient oServicepost = new PostpaidService.PostpaidServiceClient();
        private readonly CommonService.CommonServiceClient oServiceCommon = new CommonService.CommonServiceClient();
        private readonly Claro.Helpers.ExcelHelper oExcelHelper = new Claro.Helpers.ExcelHelper();




        public ActionResult PhoneList(string strIdSession, string strCustomerID, string strFlagPlataform, string strCsIdPub, string strFlagConvivencia, string strTipoProducto)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel objDataLineServiceModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel();
            Claro.Web.Logging.Info(strIdSession, "PhoneList", string.Format("strTipoProducto: {0}:", strTipoProducto));
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            if (!string.IsNullOrEmpty(strCustomerID))
            {
                try
                {
                    if (strFlagPlataform.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase) && strTipoProducto.Equals(ConfigurationManager.AppSettings("strOrigenRegistroTOBE")))
                    {
                        Claro.SIACU.Web.WebApplication.PostpaidService.obtenerProductosXCustomerResponse objresponse = new Claro.SIACU.Web.WebApplication.PostpaidService.obtenerProductosXCustomerResponse();
                        Claro.SIACU.Web.WebApplication.PostpaidService.obtenerProductosXCustomerRequest objrequest = new Claro.SIACU.Web.WebApplication.PostpaidService.obtenerProductosXCustomerRequest();
                        objrequest.idCustomer = strCustomerID;
                        objrequest.audit = audit;

                        objresponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.obtenerProductosXCustomerResponse>(() =>
                        { return oServicepost.GetProductosXCustomer(objrequest); });

                        Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel objService = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel();

                        if (objresponse.responseData.datosHFC != null)
                        {
                            foreach (var item in objresponse.responseData.datosHFC)
                            {
                                Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DataLineService.DataLineService objServices =
                                new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DataLineService.DataLineService();

                                objServices.ContractID = item.coId;
                                objServices.StateLine = item.estado;
                                objServices.Plan = item.planTarifario;

                                objServices.TypeProduct = Claro.SIACU.Constants.HFC;
                                objServices.TelephonyValue = item.telefonia;
                                objServices.InternetValue = item.internet;
                                objServices.CableValue = item.cable;
                                objServices.Telephony = item.telefonia == Claro.Constants.LetterF ? "glyphicon glyphicon-remove" : item.telefonia == Claro.Constants.LetterT ? "glyphicon glyphicon-ok" : "";
                                objServices.Internet = item.internet == Claro.Constants.LetterF ? "glyphicon glyphicon-remove" : item.internet == Claro.Constants.LetterT ? "glyphicon glyphicon-ok" : "";
                                objServices.Cable = item.cable == Claro.Constants.LetterF ? "glyphicon glyphicon-remove" : item.cable == Claro.Constants.LetterT ? "glyphicon glyphicon-ok" : "";
                                objService.lstHfcServices.Add(objServices);
                            }
                            objDataLineServiceModel.lstHfcServices = objService.lstHfcServices;
                        }
                    }
                    else
                    {
                        List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = GetDataServiceLine(strCustomerID, audit, strFlagPlataform, strCsIdPub, strFlagConvivencia);
                        objDataLineServiceModel = DataLineServiceModel(lstLine);
                    }                    
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }
            }

            return PartialView(objDataLineServiceModel);
        }

        private List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> GetDataServiceLine(string strCustomerID, PostpaidService.AuditRequest audit, string plataforma, string strCsIdPub, string strFlagConvivencia)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid()
            {
                CustomerID = int.Parse(strCustomerID),
                audit = audit,
                plataformaAT = plataforma,
                csIdPub = strCsIdPub,
                flagConvivencia = strFlagConvivencia
            };
            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid>(() =>
            { return oServicepost.GetServiceByCustomerCode(objRequest); });

            return objResponse.ListService;
        }

        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel DataLineServiceModel(List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel objService = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel();
            if (lstLine != null)
            {
                foreach (var item in lstLine)
                {
                    Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DataLineService.DataLineService objServices =
                     new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DataLineService.DataLineService();

                    objServices.CellPhone = item.NRO_CELULAR;
                    objServices.ContractID = item.CONTRATO_ID;
                    objServices.StateLine = item.ESTADO_LINEA;
                    objServices.Plan = item.PLAN;
                    objServices.ActivationDate = item.FEC_ACTIVACION;
                    objServices.DeactivationDate = item.FEC_DESACTIVACION;
                    objServices.MSISDN = item.MSISDN;
                    objServices.coidpub = item.COID_PUB;


                    if (item.TIPO_PRODUCTO != null)
                    {
                        if (item.TIPO_PRODUCTO.Equals(Claro.SIACU.Constants.Post) || item.TIPO_PRODUCTO.Equals(Claro.SIACU.Constants.Mobile))
                        {
                            objServices.TypeProduct = Claro.SIACU.Constants.PostpaidMajuscule;
                            objService.lstPostServices.Add(objServices);
                        }
                        else if (item.TIPO_PRODUCTO.Equals(ConfigurationManager.AppSettings("gConstTipoINT")))
                        {
                            objServices.TypeProduct = Claro.SIACU.Constants.IFI;
                            objService.lstPostServices.Add(objServices);
                        }
                        else if (item.TIPO_PRODUCTO.Equals(Claro.SIACU.Constants.HFC) || item.TIPO_PRODUCTO.Equals(Claro.SIACU.Constants.LTE))
                        {
                            objServices.TypeProduct = item.TIPO_PRODUCTO;
                            objServices.TelephonyValue = item.TELEFONIA;
                            objServices.InternetValue = item.INTERNET;
                            objServices.CableValue = item.CABLETV;
                            objServices.Telephony = item.TELEFONIA == Claro.Constants.LetterF ? "glyphicon glyphicon-remove" : item.TELEFONIA == Claro.Constants.LetterT ? "glyphicon glyphicon-ok" : "";
                            objServices.Internet = item.INTERNET == Claro.Constants.LetterF ? "glyphicon glyphicon-remove" : item.INTERNET == Claro.Constants.LetterT ? "glyphicon glyphicon-ok" : "";
                            objServices.Cable = item.CABLETV == Claro.Constants.LetterF ? "glyphicon glyphicon-remove" : item.CABLETV == Claro.Constants.LetterT ? "glyphicon glyphicon-ok" : "";
                            objService.lstHfcServices.Add(objServices);
                        }
                    }
                    else
                    {
                        objService.lstServices.Add(objServices);
                    }



                }
            }

            return objService;
        }

        public ActionResult InstallationDirection(string strIdSession, string strContractID, string strApplication, string strUserId)
        {

            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataCustomerModel objCustomerModel = null;
            if (strApplication.Equals(Claro.SIACU.Constants.PostpaidMajuscule))
            {
                try
                {
                    string strNroCellphone = strContractID;
                    Claro.SIACU.Web.WebApplication.PostpaidService.CustomerRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.CustomerRequestPostPaid()
                    {
                        NumCellphone = strNroCellphone,
                        audit = audit,
                    };

                    Claro.SIACU.Web.WebApplication.PostpaidService.CustomerResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.CustomerResponsePostPaid>(() =>
                    { return oServicepost.GetInstallationAddress(objRequest); });

                    objCustomerModel = DataCustomerModel(objResponse.ObjCustomer);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }
            }
            else
            {
                DashboardService.CustomerRequestDashboard objCustomerRequestDashboard = new DashboardService.CustomerRequestDashboard()
                {
                    TypeSearch = Claro.Constants.NumberThreeString,
                    ValueSearch = strContractID,
                    ApplicationType = strApplication,
                    UserId = Convert.ToInt(strUserId)
                };
                DashboardService.CustomerResponseDashboard objCustomerResponseDashboard = new DashboardService.CustomerResponseDashboard();

                try
                {
                    objCustomerRequestDashboard.audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
                    objCustomerResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.CustomerResponseDashboard>(() => { return oService.GetCustomer(objCustomerRequestDashboard); });
                    DashboardService.CustomerPostPaid objCustomer = (DashboardService.CustomerPostPaid)objCustomerResponseDashboard.InterfaceCustomer;

                    if (objCustomer != null)
                    {
                        objCustomerModel = new PostpaidController().DataCustomerModel(objCustomer, strContractID, objCustomerResponseDashboard.ApplicationType);
                        if (objCustomerModel.ActivationDate != null && objCustomerModel.ActivationDate.Trim().Length > 0)
                        {
                            objCustomerModel.ActivationDate = objCustomerModel.ActivationDate.Split(' ')[0];
                        }
                    }
                    else
                    {
                        objCustomerModel = new Models.Postpaid.DataCustomerModel();
                    }
                    strApplication = objCustomerResponseDashboard.ApplicationType;

                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objCustomerRequestDashboard.audit.Session, objCustomerRequestDashboard.audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }
            }

            objCustomerModel.Application = strApplication;

            return PartialView(objCustomerModel);
        }


        private Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataCustomerModel DataCustomerModel(Claro.SIACU.Web.WebApplication.PostpaidService.CustomerPostPaid objcustomer)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataCustomerModel objPostDataCustomer = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataCustomerModel()
            {
                Telephone = objcustomer.TELEFONO,
                Address = objcustomer.DIRECCION,
                AddressNote = objcustomer.NOTA_DIRECCION,
                District = objcustomer.DISTRITO,
                Province = objcustomer.PROVINCIA,
                Departament = objcustomer.DEPARTAMENTO,
                Country = objcustomer.PAIS
            };

            return objPostDataCustomer;
        }

        public ActionResult PhoneListAlt(string strIdSession, string strContractID, string strApplication)
        {
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            Dashboard.Models.Postpaid.DataLineServiceModel objDataLineServiceModel = new Dashboard.Models.Postpaid.DataLineServiceModel();
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Inicio PhoneListAlt");
            if (!string.IsNullOrEmpty(strContractID))
            {
                try
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, "Invocacion GetPhoneListAlt");
                    List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = GetPhoneListAlt(strContractID, strApplication, audit);
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, "Ejecucion GetPhoneListAlt");
                    objDataLineServiceModel = DataLineServiceModel(lstLine);
                    objDataLineServiceModel.ContractID = strContractID;
                    objDataLineServiceModel.Application = strApplication;
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(strIdSession);
                }
            }
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Fin PhoneListAlt");
            return PartialView(objDataLineServiceModel);
        }

        //INICIATIVA-616
        public ActionResult PhoneListAltTobe(string strIdSession, string strContractID, string strApplication, string plataforma)
        {
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            Dashboard.Models.Postpaid.DataLineServiceModel objDataLineServiceModel = new Dashboard.Models.Postpaid.DataLineServiceModel();
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Inicio PhoneListAltTobe");
            if (!string.IsNullOrEmpty(strContractID))
            {
                try
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, "Invocacion GetPhoneListAltTobe");
                    List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = GetPhoneListAltTobe(strContractID, strApplication, plataforma, audit);
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, "Ejecucion GetPhoneListAltTobe");
                    objDataLineServiceModel = DataLineServiceModel(lstLine);
                    objDataLineServiceModel.ContractID = strContractID;
                    objDataLineServiceModel.Application = strApplication;
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(strIdSession);
                }
            }
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Fin PhoneListAltTobe");
            return PartialView("PhoneListAlt", objDataLineServiceModel);
        }

        public JsonResult PhoneListAltSearch(string strIdSession, string strContractID, string strApplication)
        {

            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel objDataLineServiceModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Inicio PhoneListAltSearch");
            if (!strContractID.Equals(""))
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, "Invocacion GetPhoneListAlt");
                List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = GetPhoneListAlt(strContractID, strApplication, audit);
                Claro.Web.Logging.Error(strIdSession, audit.transaction, "Ejecucion GetPhoneListAlt");
                objDataLineServiceModel = DataLineServiceModel(lstLine);
                objDataLineServiceModel.ContractID = strContractID;
            }
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Fin PhoneListAltSearch");
            return Json(new { response = objDataLineServiceModel.lstServices });
        }

        public JsonResult PhoneListAltSearchTOBE(string strIdSession, string strContractID, string strApplication, string plataforma)
        {

            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel objDataLineServiceModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Inicio PhoneListAltSearchTOBE");
            if (!strContractID.Equals(""))
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, "Invocacion GetPhoneListAltTobe");
                List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = GetPhoneListAltTobe(strContractID, strApplication, plataforma ,audit);
                Claro.Web.Logging.Error(strIdSession, audit.transaction, "Ejecucion GetPhoneListAltTobe");
                Claro.Web.Logging.Error(strIdSession, audit.transaction, "lstLine : " + lstLine.Count.ToString());
                objDataLineServiceModel = DataLineServiceModel(lstLine);
                objDataLineServiceModel.ContractID = strContractID;
                Claro.Web.Logging.Error(strIdSession, audit.transaction, "objDataLineServiceModel.lstServices : " + objDataLineServiceModel.lstServices.Count.ToString());
            }
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Fin PhoneListAltSearchTOBE");
            return Json(new { response = objDataLineServiceModel.lstServices });
        }

        private List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> GetPhoneListAlt(string ContractID, string strApplication, PostpaidService.AuditRequest objAudit)
        {

            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid()
            {
                Application = strApplication,
                ContractID = ContractID,
                audit = objAudit
            };
            Claro.Web.Logging.Error(objRequest.audit.Session, objRequest.audit.transaction, "Inicio GetPhoneListAlt");
            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid>(() =>
            { return oServicepost.GetTelephoneByContractCode(objRequest); });
            Claro.Web.Logging.Error(objRequest.audit.Session, objRequest.audit.transaction, "Fin GetPhoneListAlt");
            return objResponse.ListService;
        }

        private List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> GetPhoneListAltTobe(string ContractID, string strApplication, string plataforma, PostpaidService.AuditRequest objAudit)
        {

            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid()
            {
                Application = strApplication,
                ContractID = ContractID,
                audit = objAudit,
                plataformaAT = plataforma
            };
            Claro.Web.Logging.Error(objRequest.audit.Session, objRequest.audit.transaction, "Inicio GetPhoneListAlt");
            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid>(() =>
            { return oServicepost.GetTelephoneByContractCode(objRequest); });
            Claro.Web.Logging.Error(objRequest.audit.Session, objRequest.audit.transaction, "Fin GetPhoneListAlt");
            return objResponse.ListService;
        }

        public ActionResult PhoneListAltHistory(string strIdSession, string strContractID, string strApplication)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel objDataLineServiceModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Inicio PhoneListAltHistory");
            if (!strContractID.Equals(""))
            {
                try
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, "Invocacion GetPhoneListAltHistory");
                    List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = GetPhoneListAltHistory(strContractID, strApplication, audit);
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, "Ejecucion exitosa GetPhoneListAltHistory");
                    objDataLineServiceModel = DataLineServiceModel(lstLine);
                    objDataLineServiceModel.Application = strApplication;
                    objDataLineServiceModel.ContractID = strContractID;
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(strIdSession);
                }
            }
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Fin PhoneListAltHistory");
            return PartialView(objDataLineServiceModel);
        }

        private List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> GetPhoneListAltHistory(string ContractID, string strApplication, PostpaidService.AuditRequest audit)
        {

            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid()
            {
                Application = strApplication,
                ContractID = ContractID,
                audit = audit,
            };
            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid>(() =>
            { return oServicepost.GetLineDisableByContractCode(objRequest); });
            return objResponse.ListService;
        }

        public JsonResult PhoneListAltHistorySearch(string strIdSession, string strContractID, string strApplication)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel objDataLineServiceModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Inicio PhoneListAltHistorySearch");
            if (!strContractID.Equals(""))
            {
                try
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, "Invocacion GetPhoneListAltHistory");
                    List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = GetPhoneListAltHistory(strContractID, strApplication, audit);
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, "Ejecucion GetPhoneListAltHistory");
                    objDataLineServiceModel = DataLineServiceModel(lstLine);
                    objDataLineServiceModel.Application = strApplication;
                    objDataLineServiceModel.ContractID = strContractID;
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }
            }
            Claro.Web.Logging.Error(strIdSession, audit.transaction, "Fin PhoneListAltHistorySearch");
            return Json(new { response = objDataLineServiceModel.lstServices });
        }
        public JsonResult getLastChangePlan(string strIdSession, string strContractID, string strCoID, string strFlagPlataform, string strFlagConvivencia)
        {
            int rows = 0;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = null;
            if (strContractID == null) strContractID = "";
            if (!strContractID.Equals(""))
            {
                try
                {

                    lstLine = GetHistoryServiceLine(strContractID, audit, strCoID, strFlagPlataform, strFlagConvivencia);
                    if (lstLine != null)
                    {
                        rows = lstLine.Count;
                        if (rows > 1)
                            lstLine = lstLine.OrderByDescending(a => a.VALIDO_DESDE).ToList();

                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }
            }
            var jsonData = new { dataRow = rows, dataList = lstLine };
            return Json(jsonData);

        }

        public ActionResult PlanHistory(string strIdSession, string strContractID, string strCoID, string strFlagPlataform, string strFlagConvivencia)
        {
            Dashboard.Models.Postpaid.DataLineServiceModel objDataLineServiceModel = new Dashboard.Models.Postpaid.DataLineServiceModel();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            if (strContractID == null) strContractID = "";
            if (!strContractID.Equals(""))
            {
                try
                {
                    // Se agrega el flap de convivencia
                    List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = GetHistoryServiceLine(strContractID, audit, strCoID, strFlagPlataform, strFlagConvivencia);
                    objDataLineServiceModel = DataHistoryServiceLineModel(lstLine);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }
            }

            return PartialView(objDataLineServiceModel);
        }

        private List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> GetHistoryServiceLine(string ContractID, PostpaidService.AuditRequest audit, string coId, string plataformaAT, string flagConvivencia) // Se agrega el parametro para el flap de convivencia.
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid()
            {
                ContractID = ContractID,
                audit = audit,
                coId = coId,
                plataformaAT = plataformaAT,
                flagConvivencia = flagConvivencia
            };

            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid>(() =>
            { return oServicepost.GetHistoryServiceLine(objRequest); });
            return objResponse.ListService;
        }

        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel DataHistoryServiceLineModel(List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel objService = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel();
            foreach (var item in lstLine)
            {
                objService.lstServices.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DataLineService.DataLineService()
                {
                    ValidFrom = item.VALIDO_DESDE,
                    PlanTariff = item.PLAN_TARIFARIO,
                    ChangedBy = item.CAMBIADO_POR,
                });
            }

            return objService;
        }

        public ActionResult ActiveDays(string strIdSession, string strContractID, string strTelefono)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.ActiveDaysModel objActiveDaysModel = null;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

            if (!strContractID.Equals(""))
            {
                Claro.SIACU.Web.WebApplication.PostpaidService.ActiveDaysResponsePostPaid objActiveDays;

                try
                {
                    objActiveDays = GetActiveDisableDays(strContractID, audit);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }

                objActiveDaysModel = DataActiveDisableDaysModel(strIdSession, objActiveDays, strTelefono);
            }

            return PartialView(objActiveDaysModel);
        }

        private Claro.SIACU.Web.WebApplication.PostpaidService.ActiveDaysResponsePostPaid GetActiveDisableDays(string strContractID, PostpaidService.AuditRequest audit)
        {
            PostpaidService.ActiveDaysRequestPostPaid objRequest = new PostpaidService.ActiveDaysRequestPostPaid() { ContratoID = strContractID, audit = audit };
            Claro.SIACU.Web.WebApplication.PostpaidService.ActiveDaysResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ActiveDaysResponsePostPaid>(() =>
            {
                return oServicepost.GetActiveDisableDays(objRequest);
            });
            return objResponse;
        }

        private Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.ActiveDaysModel DataActiveDisableDaysModel(string strIdSession, Claro.SIACU.Web.WebApplication.PostpaidService.ActiveDaysResponsePostPaid objActiveDays, string strTelefono)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.ActiveDaysModel objActiveDaysModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.ActiveDaysModel()
            {
                ActiveDays = objActiveDays.ActiveDays,
                DisableDays = objActiveDays.DisableDays,
                Message = objActiveDays.Message,
            };

            if (objActiveDays.MessageError.Equals(Claro.SIACU.Constants.OK))
            {
                SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);

                try
                {
                    string strFecha = String.Format("{0}/{1}/{2} {3}:{4}:{5}", DateTime.Now.Day.ToString("00"), DateTime.Now.Month.ToString("00"), DateTime.Now.Year, DateTime.Now.Hour.ToString("00"), DateTime.Now.Minute.ToString("00"), DateTime.Now.Second);
                    string strDesTrans = Claro.SIACU.Constants.ObtainedDaysActiveDateHour + strFecha + Claro.SIACU.Constants.NumberTelephone + strTelefono;
                    Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strTelefono, KEY.AppSettings("strAudiTraCodConsDiasActDesact"), strDesTrans); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objaudit.Session, objaudit.transaction, ex.Message);
                }
            }
            else
            {
                objActiveDaysModel.MessageError = KEY.AppSettings("strMsjeErrorConsulta");
                objActiveDaysModel.StateMessageError = true;
            }

            return objActiveDaysModel;
        }

        public ActionResult HistoricoSIM(string strIdSession, string strContractID, string strTelephone, string plataforma, string flagconvivencia, string FechaMigracion)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.HistorySIMModel objHistorySIMModel = null;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            if (strContractID == null) strContractID = "";
            if (!strContractID.Equals(""))
            {
                try
                {
                    Claro.SIACU.Web.WebApplication.PostpaidService.HistorySIMResponsePostPaid objHistorySIM = GetHistorySIM(strContractID, strTelephone, plataforma, flagconvivencia, FechaMigracion, audit);
                    objHistorySIMModel = DataHistorySIMModel(objHistorySIM);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }
            }

            return PartialView(objHistorySIMModel);
        }

        private Claro.SIACU.Web.WebApplication.PostpaidService.HistorySIMResponsePostPaid GetHistorySIM(string ContractID, string strTelephone, string plataforma, string flagConvivencia, string FechaMigracion, PostpaidService.AuditRequest audit)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.HistorySIMRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.HistorySIMRequestPostPaid()
            {
                ContractID = ContractID,
                Telephone = strTelephone,
                audit = audit,
                strFechaMigracion = FechaMigracion,
                //strFechaMigracion = string.Empty,
                strPlataforma = plataforma,
                flagconvivencia = flagConvivencia

            };
            Claro.SIACU.Web.WebApplication.PostpaidService.HistorySIMResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.HistorySIMResponsePostPaid>(() =>
            { return oServicepost.GetHistorySIM(objRequest); });
            return objResponse;
        }


        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.HistorySIMModel DataHistorySIMModel(Claro.SIACU.Web.WebApplication.PostpaidService.HistorySIMResponsePostPaid objHistorySIMResponsePostPaid)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.HistorySIMModel objHistorySIMModels = new Models.PostpaidDataService.HistorySIMModel();
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.HistorySIM.HistorySIM> lstHistorySIM = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.HistorySIM.HistorySIM>();
            if (objHistorySIMResponsePostPaid.ListHistorySIM != null)
            {
                foreach (var item in objHistorySIMResponsePostPaid.ListHistorySIM)
                {
                    lstHistorySIM.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.HistorySIM.HistorySIM()
                    {
                        State = item.Estado,
                        Reason = item.Motivo,
                        ValidFrom = item.Valido_Desde,
                        IMSI = item.IMSI,
                        ICCID = item.ICCID,
                        IntroducedTo = item.IntroducidoAl,
                        Modified = item.Modificado,
                        IntroducedBy = item.IntroducidoPor
                    });
                }
            }
            objHistorySIMModels.lstHistorySIM = lstHistorySIM;
            return objHistorySIMModels;
        }

        public ActionResult Equipments(string strIdSession, string strContractID, string strCustomerID, string strApplication)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.DecoModel objDecoModel = null;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            if (!strContractID.Equals(""))
            {
                try
                {
                    Claro.SIACU.Web.WebApplication.PostpaidService.DecoResponsePostPaid objDecoResponse = GetHistoryEquipments(strContractID, strCustomerID, strApplication, audit);
                    objDecoModel = HistoryEquipmentsModel(objDecoResponse);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }
            }
            try
            {
                SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strContractID, KEY.AppSettings("strAudiTraCodConsultaEquipos"), Claro.SIACU.Constants.Annotations); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            return PartialView(objDecoModel);
        }

        private Claro.SIACU.Web.WebApplication.PostpaidService.DecoResponsePostPaid GetHistoryEquipments(string strContractID, string strCustomerID, string strApplication, PostpaidService.AuditRequest objAudit)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.DecoRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.DecoRequestPostPaid()
            {
                ContractID = strContractID,
                CustomerID = strCustomerID,
                Application = strApplication,
                audit = objAudit
            };
            Claro.SIACU.Web.WebApplication.PostpaidService.DecoResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.DecoResponsePostPaid>(() =>
            { return oServicepost.GetHistoryEquipments(objRequest); });

            return objResponse;
        }

        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.DecoModel HistoryEquipmentsModel(Claro.SIACU.Web.WebApplication.PostpaidService.DecoResponsePostPaid objDecoResponsePostPaid)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DecoHelper.DataDeco objDecoModel = null;
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.DecoModel objDecoModels = new Models.PostpaidDataService.DecoModel();
            objDecoModels.lstDecoModel = new List<Helpers.Postpaid.DecoHelper.DataDeco>();
            if (objDecoResponsePostPaid.ListDeco != null)
            {
                foreach (var item in objDecoResponsePostPaid.ListDeco)
                {
                    objDecoModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DecoHelper.DataDeco()
                    {
                        State = Convert.ToString(item.Estado),
                        MaterialCode = Convert.ToString(item.codigo_material),
                        SapCode = Convert.ToString(item.codigo_sap),
                        SerieNumber = Convert.ToString(item.numero_serie),
                        AdressMac = Convert.ToString(item.macadress),
                        MaterialDescripcion = Convert.ToString(item.descripcion_material),
                        EquipmentType = Convert.ToString(item.tipo_equipo),
                        Center = Convert.ToString(item.centro),
                        Type = Convert.ToString(item.Tipo),
                        SimCard = Convert.ToString(item.sim_card),
                        ServiceType = Convert.ToString(item.tipo_servicio),
                        Number = Convert.ToString(item.numero),
                        ProductId = Convert.ToString(item.id_producto),
                        Model = Convert.ToString(item.modelo),
                        ConvertType = Convert.ToString(item.convertertype),
                        PricipalService = Convert.ToString(item.servicio_principal),
                        Headend = Convert.ToString(item.headend),
                        EphomeexChange = Convert.ToString(item.ephomeexchange)

                    };
                    objDecoModels.lstDecoModel.Add(objDecoModel);
                }

                objDecoModels.Title = objDecoResponsePostPaid.Title;
            }

            return objDecoModels;
        }

        public JsonResult IMRConsult(string strIdSession, string ContractID, string strTelephone, string strCustomerID, string strAccount, string strSegment, string strCodCustomerType, int intBillingCycle)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.IMRResponsePostPaid objIMRResponse;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            try
            {
                objIMRResponse = GetIMRConsult(ContractID, strTelephone, strCustomerID, strAccount, strSegment, strCodCustomerType, intBillingCycle, audit);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                objIMRResponse = new PostpaidService.IMRResponsePostPaid();
                objIMRResponse.IMRAmount = "";
            }

            return Json(new { data = objIMRResponse.IMRAmount, message = KEY.AppSettings("strMsgConsultaIMR") });
        }

        private Claro.SIACU.Web.WebApplication.PostpaidService.IMRResponsePostPaid GetIMRConsult(string ContractID, string strTelephone, string strCustomerID, string strAccount, string strSegment, string strCodCustomerType, int intBillingCycle, PostpaidService.AuditRequest objAudit)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.IMRRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.IMRRequestPostPaid()
            {
                Msisdn = strTelephone,
                CustomerId = strCustomerID,
                Account = strAccount,
                BillingCycle = intBillingCycle,
                ProductType = KEY.AppSettings("strCodApliPost"),
                CustomerType = strCodCustomerType,
                Segment = strSegment,
                ContractId = ContractID,
                audit = objAudit
            };
            Claro.SIACU.Web.WebApplication.PostpaidService.IMRResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.IMRResponsePostPaid>(() =>
            { return oServicepost.GetIMRConsult(objRequest); });

            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>((objAudit.Session));
            try
            {
                string strText = Claro.SIACU.Constants.ConsultIMR + strTelephone + Claro.SIACU.Constants.CustomerCode + strCustomerID;
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strTelephone, KEY.AppSettings("strAudiTraCodConsultaIMR"), strText); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objaudit.Session, objaudit.transaction, ex.Message);
            }

            return objResponse;
        }

        public ActionResult Services()
        {
            return PartialView();
        }

        public JsonResult GetPhoneContract(string strIdSession, string plataformaAT, PostpaidService.ContractedBusinessServicesRequestPostPaid objContractedBusinessServicesRequestPostPaid)
        {
            PostpaidService.ContractedBusinessServicesResponsePostPaid objContractedBusinessServicesResponsePostPaid = null;

            try
            {
                objContractedBusinessServicesRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objContractedBusinessServicesResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.ContractedBusinessServicesResponsePostPaid>(() => { return oServicepost.GetPhoneContract(objContractedBusinessServicesRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objContractedBusinessServicesRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objContractedBusinessServicesRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.ContractedBusinessServicesModel objPhoneContractModel = new Models.PostpaidDataService.ContractedBusinessServicesModel();

            if (objContractedBusinessServicesResponsePostPaid.PhoneContracts != null)
            {
                List<Helpers.Postpaid.ContractedBusinessServicesHelper.PhoneContract> listPhoneContracts = new List<Helpers.Postpaid.ContractedBusinessServicesHelper.PhoneContract>();

                foreach (PostpaidService.PhoneContractPostPaid objPhoneContractPostPaid in objContractedBusinessServicesResponsePostPaid.PhoneContracts)
                {
                    listPhoneContracts.Add(new Helpers.Postpaid.ContractedBusinessServicesHelper.PhoneContract()
                    {
                        CustomerCode = objPhoneContractPostPaid.CUSTCODE,
                        ContractId = objPhoneContractPostPaid.COID,
                        Name = objPhoneContractPostPaid.NOMBRE,
                        State = objPhoneContractPostPaid.ESTADO,
                        Date = objPhoneContractPostPaid.FECHA,
                        Reason = objPhoneContractPostPaid.RAZON,
                        ContractIdPub = objPhoneContractPostPaid.COID_PUB,
                        origen = objPhoneContractPostPaid.origen
                    });
                }
                objPhoneContractModel.PhoneContracts = listPhoneContracts;
            }

            return Json(new { data = objPhoneContractModel });
        }

        public JsonResult GetContractServices(string strIdSession, string plataformaAT, PostpaidService.ContractedBusinessServicesRequestPostPaid objContractedBusinessServicesRequestPostPaid)
        {
            PostpaidService.ContractedBusinessServicesResponsePostPaid objContractedBusinessServicesResponsePostPaid = null;
            string strText = "";
            try
            {
                objContractedBusinessServicesRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objContractedBusinessServicesResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.ContractedBusinessServicesResponsePostPaid>(() => { return oServicepost.GetContractServices(objContractedBusinessServicesRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objContractedBusinessServicesRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objContractedBusinessServicesRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.ContractedBusinessServicesModel objContractedBusinessServicesModel = new Models.PostpaidDataService.ContractedBusinessServicesModel();

            if (objContractedBusinessServicesResponsePostPaid.ContractServices != null)
            {
                List<Helpers.Postpaid.ContractedBusinessServicesHelper.ContractServices> listContractServices = new List<Helpers.Postpaid.ContractedBusinessServicesHelper.ContractServices>();

                foreach (PostpaidService.ContractServicesPostPaid objContractServicesPostPaid in objContractedBusinessServicesResponsePostPaid.ContractServices)
                {
                    listContractServices.Add(new Helpers.Postpaid.ContractedBusinessServicesHelper.ContractServices()
                    {
                        GroupCode = objContractServicesPostPaid.COD_GRUPO,
                        GroupDescription = objContractServicesPostPaid.DES_GRUPO,
                        GroupPos = objContractServicesPostPaid.POS_GRUPO,
                        ServiceCode = objContractServicesPostPaid.COD_SERV,
                        ServiceDescription = objContractServicesPostPaid.DES_SERV,
                        ServicePos = objContractServicesPostPaid.POS_SERV,
                        ExclusiveCode = objContractServicesPostPaid.COD_EXCLUYENTE,
                        ExclusiveDescription = objContractServicesPostPaid.DES_EXCLUYENTE,
                        State = objContractServicesPostPaid.ESTADO,
                        ValidityDate = objContractServicesPostPaid.FECHA_VALIDEZ,
                        SubscriptionFeeAmount = objContractServicesPostPaid.MONTO_CARGO_SUS,
                        AmountFixedCharge = objContractServicesPostPaid.MONTO_CARGO_FIJO,
                        ModifiedShare = objContractServicesPostPaid.CUOTA_MODIF,
                        FinalAmount = objContractServicesPostPaid.MONTO_FINAL,
                        ValidPeriods = objContractServicesPostPaid.PERIODOS_VALIDOS,
                        DisableLock = objContractServicesPostPaid.BLOQUEO_DESACT,
                        ActiveBlocking = objContractServicesPostPaid.BLOQUEO_ACT,
                    });
                }
                objContractedBusinessServicesModel.ContractServices = listContractServices;
                strText = Claro.SIACU.Constants.ReturnContractService;
            }
            else
            {
                strText = Claro.SIACU.Constants.ReturnContractServiceNull;
            }


            if (!objContractedBusinessServicesRequestPostPaid.Application.Equals(Claro.SIACU.Constants.PostpaidMajuscule))
            {

                SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
                try
                {
                    strText = strText + objContractedBusinessServicesRequestPostPaid.Telephone + Claro.SIACU.Constants.ContractCode + objContractedBusinessServicesRequestPostPaid.ContractId;
                    Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, objContractedBusinessServicesRequestPostPaid.Telephone, KEY.AppSettings("strAudiTraCodConsultaServComercial"), strText); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
                }
            }

            return Json(new { data = objContractedBusinessServicesModel });
        }

        public ActionResult ServicesBSCS()
        {
            return PartialView();
        }

        public JsonResult GetServicesBSCS(string strIdSession, PostpaidService.ContractedBusinessServicesRequestPostPaid objContractedBusinessServicesRequestPostPaid)
        {
            PostpaidService.ContractedBusinessServicesResponsePostPaid objContractedBusinessServicesResponsePostPaid = null;

            try
            {
                objContractedBusinessServicesRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objContractedBusinessServicesResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.ContractedBusinessServicesResponsePostPaid>(() => { return oServicepost.GetServiceBSCS(objContractedBusinessServicesRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objContractedBusinessServicesRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objContractedBusinessServicesRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.ContractedBusinessServicesModel objServiceBSCSModel = new Models.PostpaidDataService.ContractedBusinessServicesModel();

            if (objContractedBusinessServicesResponsePostPaid.BSCSServices != null)
            {
                List<Helpers.Postpaid.ContractedBusinessServicesHelper.BSCSService> listBSCSServices = new List<Helpers.Postpaid.ContractedBusinessServicesHelper.BSCSService>();

                foreach (PostpaidService.ServiceBSCSPostPaid objServiceBSCSPostPaid in objContractedBusinessServicesResponsePostPaid.BSCSServices)
                {
                    listBSCSServices.Add(new Helpers.Postpaid.ContractedBusinessServicesHelper.BSCSService()
                    {
                        Service = objServiceBSCSPostPaid.SERVICIO,
                        Package = objServiceBSCSPostPaid.PAQUETE,
                        State = objServiceBSCSPostPaid.ESTADO
                    });
                }
                objServiceBSCSModel.BSCSServices = listBSCSServices;
            }

            return Json(new { data = objServiceBSCSModel });
        }

        public ActionResult ComputerQuery()
        {
            return PartialView();
        }

        public JsonResult GetComputerQuery(string strIdSession, PostpaidService.ComputerQueryRequestPostPaid objComputerQueryRequestPostPaid)
        {
            PostpaidService.ComputerQueryResponsePostPaid objComputerQueryResponsePostPaid;

            try
            {
                objComputerQueryRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objComputerQueryResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.ComputerQueryResponsePostPaid>(() => { return oServicepost.GetComputerQuery(objComputerQueryRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objComputerQueryRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objComputerQueryRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.DecosModel objDecosModel = new Models.PostpaidDataService.DecosModel();

            if (objComputerQueryResponsePostPaid.Decos != null)
            {
                List<Helpers.Postpaid.DecoHelper.Deco> ListDeco = new List<Helpers.Postpaid.DecoHelper.Deco>();

                foreach (PostpaidService.DecoPostPaid item in objComputerQueryResponsePostPaid.Decos)
                {
                    ListDeco.Add(new Helpers.Postpaid.DecoHelper.Deco()
                    {
                        TransactionId = item.id_transaccion,
                        MaterialCode = item.codigo_material,
                        SapCode = item.codigo_sap,
                        SerieNumber = item.numero_serie,
                        AdressMac = item.macadress,
                        DescriptionMaterial = item.descripcion_material,
                        AbbreviationMaterials = item.abrev_material,
                        MaterialState = item.estado_material,
                        StorePrice = item.precio_almacen,
                        AccountCode = item.codigo_cuenta,
                        Component = item.componente,
                        Center = item.centro,
                        IdStore = item.idalm,
                        Warehouse = item.almacen,
                        EquipmentType = item.tipo_equipo,
                        ProductId = item.id_producto,
                        CustomerId = item.id_cliente,
                        Model = item.modelo,
                        UserDate = item.fecusu,
                        UserCode = item.codusu,
                        ConvertType = item.convertertype,
                        MainService = item.servicio_principal,
                        Headend = item.headend,
                        EphomeexChange = item.ephomeexchange,
                        Number = item.numero
                    });
                }
                objDecosModel.Decos = ListDeco;
            }

            return Json(new { data = objDecosModel });
        }

        public ActionResult ScheduledTasks()
        {
            return PartialView();
        }

        public JsonResult GetStateType(string strIdSession, CommonService.StateTypeRequestCommon objStateTypeRequestPostPaid)
        {
            CommonService.StateTypeResponseCommon objStateTypeResponseCommon = null;
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            try
            {
                objStateTypeRequestPostPaid.audit = audit;
                objStateTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.StateTypeResponseCommon>(() => { return oServiceCommon.GetStateType(objStateTypeRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objStateTypeRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            Models.PostpaidDataService.ScheduledTasksModel objStateTypeModel = null;

            if (objStateTypeResponseCommon != null && objStateTypeResponseCommon.StateTypes != null)
            {
                objStateTypeModel = new Models.PostpaidDataService.ScheduledTasksModel();

                List<Helpers.Postpaid.ScheduledTasksHelper.StateType> listStateTypes = new List<Helpers.Postpaid.ScheduledTasksHelper.StateType>();

                foreach (CommonService.ListItem item in objStateTypeResponseCommon.StateTypes)
                {
                    listStateTypes.Add(new Helpers.Postpaid.ScheduledTasksHelper.StateType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objStateTypeModel.StateTypes = listStateTypes;
            }

            return Json(new { data = objStateTypeModel });
        }

        public JsonResult GetTransactionType(string strIdSession)
        {
            CommonService.TransactionTypeResponseCommon objTransactionTypeResponseCommon = null;
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            CommonService.TransactionTypeRequestCommon objTransactionTypeRequestCommon = new CommonService.TransactionTypeRequestCommon()
            {
                audit = audit
            };

            try
            {
                objTransactionTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.TransactionTypeResponseCommon>(() => { return oServiceCommon.GetTransactionType(objTransactionTypeRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objTransactionTypeRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            Models.PostpaidDataService.ScheduledTasksModel objScheduledTasksModel = null;

            if (objTransactionTypeResponseCommon != null && objTransactionTypeResponseCommon.TransactionTypes != null)
            {
                objScheduledTasksModel = new Models.PostpaidDataService.ScheduledTasksModel();
                List<Helpers.Postpaid.ScheduledTasksHelper.TransactionType> listTransactionTypes = new List<Helpers.Postpaid.ScheduledTasksHelper.TransactionType>();

                foreach (CommonService.ListItem item in objTransactionTypeResponseCommon.TransactionTypes)
                {
                    listTransactionTypes.Add(new Helpers.Postpaid.ScheduledTasksHelper.TransactionType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objScheduledTasksModel.TransactionTypes = listTransactionTypes;
            }

            return Json(new { data = objScheduledTasksModel });
        }

        public JsonResult GetCacDacType(string strIdSession)
        {
            CommonService.CacDacTypeResponseCommon objCacDacTypeResponseCommon = null;
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            CommonService.CacDacTypeRequestCommon objCacDacTypeRequestCommon = new CommonService.CacDacTypeRequestCommon()
            {
                audit = audit
            };

            try
            {
                objCacDacTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.CacDacTypeResponseCommon>(() => { return oServiceCommon.GetCacDacType(objCacDacTypeRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCacDacTypeRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            Models.PostpaidDataService.ScheduledTasksModel objScheduledTasksModel = null;

            if (objCacDacTypeResponseCommon != null && objCacDacTypeResponseCommon.CacDacTypes != null)
            {
                objScheduledTasksModel = new Models.PostpaidDataService.ScheduledTasksModel();
                List<Helpers.Postpaid.ScheduledTasksHelper.CacDacType> listCacDacTypes = new List<Helpers.Postpaid.ScheduledTasksHelper.CacDacType>();

                foreach (CommonService.ListItem item in objCacDacTypeResponseCommon.CacDacTypes)
                {
                    listCacDacTypes.Add(new Helpers.Postpaid.ScheduledTasksHelper.CacDacType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objScheduledTasksModel.CacDacTypes = listCacDacTypes;
            }

            return Json(new { data = objScheduledTasksModel });
        }

        public Models.PostpaidDataService.ScheduledTasksModel DataScheduledTransaction(string strIdSession, PostpaidService.ScheduledTransactionRequestPostPaid objScheduledTransactionRequestPostPaid)
        {
            PostpaidService.ScheduledTransactionResponsePostPaid objScheduledTransactionResponsePostPaid = null;

            try
            {
                objScheduledTransactionRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objScheduledTransactionResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.ScheduledTransactionResponsePostPaid>(() => { return oServicepost.GetScheduledTransaction(objScheduledTransactionRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objScheduledTransactionRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objScheduledTransactionRequestPostPaid.audit.transaction);
            }

            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);

            try
            {
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, objScheduledTransactionRequestPostPaid.Account, KEY.AppSettings("strAudiTraCodConsTranProg"), KEY.AppSettings("strMsgAuditTranProgC")); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
            }

            Models.PostpaidDataService.ScheduledTasksModel objScheduledTasksModel = new Models.PostpaidDataService.ScheduledTasksModel();

            if (objScheduledTransactionResponsePostPaid.ScheduledTransactions != null)
            {
                List<Helpers.Postpaid.ScheduledTasksHelper.ScheduledTransaction> listScheduledTransactions = new List<Helpers.Postpaid.ScheduledTasksHelper.ScheduledTransaction>();

                foreach (PostpaidService.ScheduledTransactionPostPaid item in objScheduledTransactionResponsePostPaid.ScheduledTransactions)
                {
                    listScheduledTransactions.Add(new Helpers.Postpaid.ScheduledTasksHelper.ScheduledTransaction()
                    {
                        CoId = item.CO_ID,
                        CustomerId = item.CUSTOMER_ID,
                        ServdDateProg = item.SERVD_FECHAPROG,
                        ServdDateReg = item.SERVD_FECHA_REG,
                        ServdDateEjec = item.SERVD_FECHA_EJEC,
                        ServcState = item.SERVC_ESTADO,
                        DescState = item.DESC_ESTADO,
                        ServcIsBatch = item.SERVC_ESBATCH,
                        ServvMenError = item.SERVV_MEN_ERROR,
                        ServvCodeError = item.SERVV_COD_ERROR,
                        ServUserSystem = item.SERVV_USUARIO_SISTEMA,
                        ServvIdEaiSw = item.SERVV_ID_EAI_SW,
                        ServiCode = item.SERVI_COD,
                        DescServi = item.DESC_SERVI,
                        ServvMsisdn = item.SERVV_MSISDN,
                        ServvIdBatch = item.SERVV_ID_BATCH,
                        ServvUserAplication = item.SERVV_USUARIO_APLICACION,
                        ServvEmailUserApp = item.SERVV_EMAIL_USUARIO_APP,
                        ServvXmlEntry = item.SERVV_XMLENTRADA,
                        ServcNumberAccount = item.SERVC_NROCUENTA,
                        ServcCodeInteraction = item.SERVC_CODIGO_INTERACCION,
                        ServcSellingPoint = item.SERVC_PUNTOVENTA,
                        ServcTypeServ = item.SERVC_TIPO_SERV,
                        ServcCoSer = item.SERVC_CO_SER,
                        ServcTypeReg = item.SERVC_TIPO_REG,
                        ServcDesCoSer = item.SERVC_DES_CO_SER
                    });
                }
                objScheduledTasksModel.ScheduledTransactions = listScheduledTransactions;
            }

            return objScheduledTasksModel;
        }

        public JsonResult GetScheduledTransaction(string strIdSession, PostpaidService.ScheduledTransactionRequestPostPaid objScheduledTransactionRequestPostPaid)
        {
            return Json(new { data = DataScheduledTransaction(strIdSession, objScheduledTransactionRequestPostPaid) });
        }

        public JsonResult ScheduledExportTransaction(string strIdSession, PostpaidService.ScheduledTransactionRequestPostPaid objScheduledTransactionRequestPostPaid)
        {
            return Json(oExcelHelper.ExportExcel(DataScheduledTransaction(strIdSession, objScheduledTransactionRequestPostPaid), TemplateExcel.CONST_SCHEDULEDTRANSACTION));
        }
        public ActionResult HistoryTreason()
        {
            return PartialView();
        }

        public ActionResult Treason()
        {
            return PartialView();
        }

        #region Peticiones
        public ActionResult Petitions()
        {
            return PartialView();
        }

        public JsonResult GetPetitionsType(string strIdSession)
        {
            PostpaidService.PetitionsResponseCommon objPetitionsResponseCommon = null;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            PostpaidService.PetitionsRequestCommon objPetitionsRequestCommon = new PostpaidService.PetitionsRequestCommon()
            {
                audit = audit
            };

            try
            {
                objPetitionsResponseCommon = Claro.Web.Logging.ExecuteMethod<PostpaidService.PetitionsResponseCommon>(() => { return oServicepost.GetPetitionsType(objPetitionsRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objPetitionsRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            Areas.Dashboard.Models.PostpaidDataService.PetitionsModel objPetitionsModal = new Areas.Dashboard.Models.PostpaidDataService.PetitionsModel();

            if (objPetitionsResponseCommon != null && objPetitionsResponseCommon.PetitionsTypes != null)
            {
                List<Helpers.Postpaid.PetitionsHelper.PetitionType> listPetitionTypes = new List<Helpers.Postpaid.PetitionsHelper.PetitionType>();

                foreach (PostpaidService.ListItem item in objPetitionsResponseCommon.PetitionsTypes)
                {
                    listPetitionTypes.Add(new Helpers.Postpaid.PetitionsHelper.PetitionType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objPetitionsModal.PetitionTypes = listPetitionTypes;

            }
            return Json(new { data = objPetitionsModal });
        }

        public JsonResult GetPetitions(string strIdSession, PostpaidService.PetitionstRequestPostPaid objPetitionstRequestPostPaid)
        {
            PostpaidService.PetitionResponsePostPaid objPetitionResponsePostPaid = null;

            try
            {
                objPetitionstRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objPetitionResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.PetitionResponsePostPaid>(() => { return oServicepost.GetPetitions(objPetitionstRequestPostPaid); });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, objPetitionstRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objPetitionstRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.PetitionsModel objPetitionsModal = new Areas.Dashboard.Models.PostpaidDataService.PetitionsModel();

            if (objPetitionResponsePostPaid.Petitions != null)
            {
                List<Helpers.Postpaid.PetitionsHelper.Petition> listPetitions = new List<Helpers.Postpaid.PetitionsHelper.Petition>();
                var lista = (from l in objPetitionResponsePostPaid.Petitions orderby l.Fecha_Peticion descending select l).ToList();
                foreach (var item in lista)
                {
                    listPetitions.Add(new Helpers.Postpaid.PetitionsHelper.Petition()
                    {
                        DateRequest = item.Fecha_Peticion.ToString("dd/MM/yyyy hh:mm:ss tt"),
                        ExpirationDate = item.Fecha_Vencimiento.ToString("dd/MM/yyyy hh:mm:ss tt"),
                        State = item.Estado,
                        Action = item.Accion,
                        User = item.Usuario,
                        OrderID = item.OrdenId

                    });
                }
                objPetitionsModal.Petitions = listPetitions;
            }

            return Json(new { data = objPetitionsModal });
        }

        #endregion

        #region Triaciones
        public ActionResult Striations()
        {
            return PartialView();
        }
        public JsonResult HistoryTreasonExport(string Application, string strIdSession, PostpaidService.HistoricalStriationsRequestPostPaid objHistoricalStriationsRequestPostPaid)
        {
            Areas.Dashboard.Models.PostpaidDataService.HistoricalStriationsModel objPinPukModel = GetHistoricalStriationsExport(Application, strIdSession, objHistoricalStriationsRequestPostPaid);
            string path = oExcelHelper.ExportExcel(objPinPukModel, TemplateExcel.CONST_HISTORY_FREQUENT);
            return Json(path);
        }
        public Areas.Dashboard.Models.PostpaidDataService.HistoricalStriationsModel GetHistoricalStriationsExport(string Application, string strIdSession, PostpaidService.HistoricalStriationsRequestPostPaid objHistoricalStriationsRequestPostPaid)
        {
            PostpaidService.HistoricalStriationsResponsePostpaid objHistoricalStriationsResponsePostpaid = null;
            PostpaidService.ServiceRequestPostPaid objServiceRequestPostPaid = new PostpaidService.ServiceRequestPostPaid();
            objServiceRequestPostPaid.ContractID = objHistoricalStriationsRequestPostPaid.ContractID;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            string strTelephone = string.Empty;
            if (Application.Equals(Claro.SIACU.Constants.HFC))
            {

                Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid()
                {
                    ContractID = objHistoricalStriationsRequestPostPaid.ContractID,
                    Application = Application,
                    IdTransaction = App_Code.Common.GetTransactionID(),
                    IpApplication = App_Code.Common.GetApplicationIp(),
                    ApplicationName = App_Code.Common.GetApplicationName(),
                    UserApplication = App_Code.Common.CurrentUser,
                    audit = audit
                };

                Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid>(
                    () =>
                    { return oServicepost.GetDataServiceLineHFC(objRequest); });

                strTelephone = objResponse.ObjService.MSISDN;
                objHistoricalStriationsRequestPostPaid.Telephone = objResponse.ObjService.MSISDN;
            }
            else
            {
                List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = GetDataServiceLine(objHistoricalStriationsRequestPostPaid.CustomerId, audit, string.Empty, string.Empty, string.Empty);
                strTelephone = lstLine[0].NRO_CELULAR.ToString();
            }



            try
            {
                objHistoricalStriationsRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objHistoricalStriationsResponsePostpaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.HistoricalStriationsResponsePostpaid>(() => { return oServicepost.GetHistoricalStriations(objHistoricalStriationsRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objHistoricalStriationsRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objHistoricalStriationsRequestPostPaid.audit.transaction);
            }

            Areas.Dashboard.Models.PostpaidDataService.HistoricalStriationsModel objHistoricalStriationsModel = new Areas.Dashboard.Models.PostpaidDataService.HistoricalStriationsModel();

            if (objHistoricalStriationsResponsePostpaid.HistoricalStriations != null)
            {
                List<Helpers.Postpaid.HistoricalStriationHelper.HistoricalStriation> listHistoricalStriations = new List<Helpers.Postpaid.HistoricalStriationHelper.HistoricalStriation>();

                foreach (PostpaidService.HistoricalStriationsPostPaid item in objHistoricalStriationsResponsePostpaid.HistoricalStriations)
                {
                    listHistoricalStriations.Add(new Helpers.Postpaid.HistoricalStriationHelper.HistoricalStriation()
                    {
                        ActivationArea = item.AREA_ACTIVACION,
                        Client = item.CLIENTE,
                        Contract = item.CONTRATO,
                        Account = item.CUENTA,
                        Description = item.DESCRIPCION,
                        Destination = item.DESTINO,
                        State = item.ESTADO,
                        Factor = item.FACTOR,
                        Date = item.FECHA,
                        Modification = item.MODIFICACION,
                        Reason = item.MOTIVO,
                        Name = item.NOMBRE,
                        TelephoneNumber = item.NUMERO_TELEFONO,
                        Origin = item.ORIGEN,
                        Plan = item.PLAN,
                        User = item.USUARIO

                    });

                }
                objHistoricalStriationsModel.HistoricalStriations = listHistoricalStriations;

            }
            objHistoricalStriationsModel.Account = objHistoricalStriationsRequestPostPaid.CustomerId;
            objHistoricalStriationsModel.Name = objHistoricalStriationsRequestPostPaid.Customer;
            objHistoricalStriationsModel.TelephoneNumber = strTelephone;
            return objHistoricalStriationsModel;
        }
        public JsonResult GetStriations(string strIdSession, PostpaidService.StriationsRequestPostPaid objStriationsRequestPostPaid)
        {
            PostpaidService.StriationsResponsePostPaid objStriationsResponsePostPaid = null;

            try
            {
                objStriationsRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objStriationsResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.StriationsResponsePostPaid>(() => { return oServicepost.GetTriaciones(objStriationsRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objStriationsRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objStriationsRequestPostPaid.audit.transaction);
            }

            Areas.Dashboard.Models.PostpaidDataService.StriationsModel objStriationsModel = new Areas.Dashboard.Models.PostpaidDataService.StriationsModel();

            if (objStriationsResponsePostPaid.Striations != null)
            {
                List<Helpers.Postpaid.StriationsHelper.Striation> listStriations = new List<Helpers.Postpaid.StriationsHelper.Striation>();

                foreach (PostpaidService.StriationsPostPaid item in objStriationsResponsePostPaid.Striations)
                {
                    listStriations.Add(new Helpers.Postpaid.StriationsHelper.Striation()
                    {
                        NumberThreesome = item.NUM_TRIO,
                        TypeDestination = item.TIPO_DESTINO,
                        Factor = item.FACTOR,
                        Telephone = item.TELEFONO,
                        TypeTriado = item.TIPO_TRIADO,
                        TrioDestination = item.DESTINO_TRIO,
                        CodeTypeDestination = item.CODIGO_TIPO_DESTINO == "1" ? "SI" : "NO"

                    });
                }
                objStriationsModel.Striations = listStriations;
            }

            return Json(new { data = objStriationsModel });
        }
        public ActionResult HistoricalStriations()
        {
            return PartialView();
        }
        public JsonResult GetHistoricalStriations(string Application, string strIdSession, PostpaidService.HistoricalStriationsRequestPostPaid objHistoricalStriationsRequestPostPaid)
        {

            PostpaidService.HistoricalStriationsResponsePostpaid objHistoricalStriationsResponsePostpaid = null;
            PostpaidService.ServiceRequestPostPaid objServiceRequestPostPaid = new PostpaidService.ServiceRequestPostPaid();
            objServiceRequestPostPaid.ContractID = objHistoricalStriationsRequestPostPaid.ContractID;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            Areas.Dashboard.Models.PostpaidDataService.HistoricalStriationsModel objHistoricalStriationsModel = new Areas.Dashboard.Models.PostpaidDataService.HistoricalStriationsModel();
            string strTelephone = string.Empty;
            if (Application.Equals(Claro.SIACU.Constants.HFC))
            {
                Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid()
                {
                    ContractID = objHistoricalStriationsRequestPostPaid.ContractID,
                    Application = Application,
                    IdTransaction = App_Code.Common.GetTransactionID(),
                    IpApplication = App_Code.Common.GetApplicationIp(),
                    ApplicationName = App_Code.Common.GetApplicationName(),
                    UserApplication = App_Code.Common.CurrentUser,
                    audit = audit
                };

                Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid>(
                    () =>
                    { return oServicepost.GetDataServiceLineHFC(objRequest); });

                strTelephone = objResponse.ObjService.MSISDN;
                objHistoricalStriationsRequestPostPaid.Telephone = objResponse.ObjService.MSISDN;
            }
            else
            {
                List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = GetDataServiceLine(objHistoricalStriationsRequestPostPaid.CustomerId, audit, string.Empty, string.Empty, string.Empty);
                strTelephone = lstLine[0].NRO_CELULAR.ToString();
            }


            try
            {

                objHistoricalStriationsRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objHistoricalStriationsResponsePostpaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.HistoricalStriationsResponsePostpaid>(() => { return oServicepost.GetHistoricalStriations(objHistoricalStriationsRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objHistoricalStriationsRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objHistoricalStriationsRequestPostPaid.audit.transaction);
            }



            if (objHistoricalStriationsResponsePostpaid.HistoricalStriations != null)
            {
                List<Helpers.Postpaid.HistoricalStriationHelper.HistoricalStriation> listHistoricalStriations = new List<Helpers.Postpaid.HistoricalStriationHelper.HistoricalStriation>();

                foreach (PostpaidService.HistoricalStriationsPostPaid item in objHistoricalStriationsResponsePostpaid.HistoricalStriations)
                {
                    listHistoricalStriations.Add(new Helpers.Postpaid.HistoricalStriationHelper.HistoricalStriation()
                    {
                        ActivationArea = item.AREA_ACTIVACION,
                        Client = item.CLIENTE,
                        Contract = item.CONTRATO,
                        Account = item.CUENTA,
                        Description = item.DESCRIPCION,
                        Destination = item.DESTINO,
                        State = item.ESTADO,
                        Factor = item.FACTOR,
                        Date = item.FECHA,
                        Modification = item.MODIFICACION,
                        Reason = item.MOTIVO,
                        Name = item.NOMBRE,
                        TelephoneNumber = item.NUMERO_TELEFONO,
                        Origin = item.ORIGEN,
                        Plan = item.PLAN,
                        User = item.USUARIO
                    });
                }
                objHistoricalStriationsModel.HistoricalStriations = listHistoricalStriations;
            }
            objHistoricalStriationsModel.Account = objHistoricalStriationsRequestPostPaid.CustomerId;
            objHistoricalStriationsModel.Name = objHistoricalStriationsRequestPostPaid.Customer;
            objHistoricalStriationsModel.TelephoneNumber = strTelephone;

            return Json(new { data = objHistoricalStriationsModel });
        }
        #endregion

        #region Consumo/Saldo

        public ActionResult ConsumptionBalance()
        {
            return PartialView();
        }
        public ActionResult OrderConsumption()
        {
            return PartialView();
        }


        public JsonResult GetRecharges(string strIdSession, PostpaidService.RechargesRequestPostPaid objRechargesRequestPostPaid)
        {
            PostpaidService.RechargesResponsePostPaid objRechargesResponsePostPaid = null;

            try
            {
                objRechargesRequestPostPaid.CurrentUser = App_Code.Common.CurrentUser;
                objRechargesRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objRechargesResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.RechargesResponsePostPaid>(() => { return oServicepost.GetRecharges(objRechargesRequestPostPaid); });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, objRechargesRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRechargesRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.ConsumptionBalanceModel objRechargesModel = new Models.PostpaidDataService.ConsumptionBalanceModel();
            List<Helpers.Postpaid.ConsumptionBalanceHelper.Recharge> listRecharges = null;
            List<Helpers.Postpaid.ConsumptionBalanceHelper.Service> listService = null;
            Helpers.Postpaid.ConsumptionBalanceHelper.Service objService = null;

            if (objRechargesResponsePostPaid.Recharges != null)
            {
                listRecharges = new List<Helpers.Postpaid.ConsumptionBalanceHelper.Recharge>();

                foreach (PostpaidService.RechargePostPaid item in objRechargesResponsePostPaid.Recharges)
                {
                    listRecharges.Add(new Helpers.Postpaid.ConsumptionBalanceHelper.Recharge()
                    {


                        Bag = item.BOLSA,
                        ExpirationDate = item.FECHA_EXPIRACION,
                        Name = item.NOMBRE,
                        Consumption = item.CONSUMO,
                        Unity = item.UNIDAD,
                        Balance = item.SALDO,
                        TypePackage = item.TIPO_PAQUETE,
                        TypeUnity = item.TIPO_UNIDAD,
                        Zone = item.ZONA_DIF,
                        Unified = item.UNIFICADA,
                        UnifiedIndex = item.INDEX_UNIFICADA
                        //BagOrderId = item.ID_ORDEN_BOLSA

                    });
                }
            }
            var responseRecarga = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(listRecharges);
            Claro.Web.Logging.Info("Session", "Consumo/Saldo", string.Format("ResponseRecarga: {0}:",responseRecarga));
            if (objRechargesResponsePostPaid.Services != null)
            {
                listService = new List<Helpers.Postpaid.ConsumptionBalanceHelper.Service>();

                foreach (PostpaidService.ServicePostPaid item in objRechargesResponsePostPaid.Services)
                {
                    listService.Add(new Helpers.Postpaid.ConsumptionBalanceHelper.Service()
                    {
                        Package = item.PAQUETE,
                        IdPackage = item.PAQUETE_ID,
                        Share = item.CUOTA
                    });
                }

                objRechargesModel.ActivationDate = objRechargesResponsePostPaid.ActivationDate;
                objRechargesModel.Message = objRechargesResponsePostPaid.Message;
            }

            if (objRechargesResponsePostPaid.Service != null)
            {
                objService = new Helpers.Postpaid.ConsumptionBalanceHelper.Service()
                {
                    CellPhoneNumber = objRechargesResponsePostPaid.Service.NRO_CELULAR,
                    PrincipalBalance = objRechargesResponsePostPaid.Service.SALDO_PRINCIPAL,
                    ExpirationDateBalance = objRechargesResponsePostPaid.Service.FECHA_EXPIRACION_SALDO,
                    StateLine = objRechargesResponsePostPaid.Service.ESTADO_LINEA,
                    FreeTriosChanges = objRechargesResponsePostPaid.Service.CAMBIOS_TRIOS_GRATIS,
                    FreeRateChanges = objRechargesResponsePostPaid.Service.CAMBIOS_TARIFA_GRATIS,
                    RatePlan = objRechargesResponsePostPaid.Service.PLAN_TARIFARIO,
                    IdProvider = objRechargesResponsePostPaid.Service.PROVIDER_ID,
                    ActivationDate = objRechargesResponsePostPaid.Service.FEC_ACTIVACION,
                    ExpirationDateLine = objRechargesResponsePostPaid.Service.FECHA_EXPIRACION_LINEA,
                    DateDol = objRechargesResponsePostPaid.Service.FECHA_DOL,
                    SubscribeState = objRechargesResponsePostPaid.Service.SUBSCRIBIR_ESTADO,
                    CntNumber = objRechargesResponsePostPaid.Service.CNT_NUMERO,
                    CntPossible = objRechargesResponsePostPaid.Service.CNT_POSIBLE,
                    NumberImsi = objRechargesResponsePostPaid.Service.NROIMSI,
                    StateImsi = objRechargesResponsePostPaid.Service.ESTADO_IMSI
                };

                if (objRechargesResponsePostPaid.Service.LISTA_ACCOUNT != null)
                {
                    foreach (PostpaidService.AccountPostPaid item in objRechargesResponsePostPaid.Service.LISTA_ACCOUNT)
                    {
                        objService.Accounts.Add(new Helpers.Postpaid.ConsumptionBalanceHelper.Account()
                        {
                            Name = item.NOMBRE,
                            Balance = item.SALDO,
                            ExpirationDate = item.FECHA_EXPIRACION
                        });
                    }
                }

                if (objRechargesResponsePostPaid.Service.LISTA_TRIO != null)
                {
                    foreach (PostpaidService.TrioPostPaid item in objRechargesResponsePostPaid.Service.LISTA_TRIO)
                    {
                        objService.Trios.Add(new Helpers.Postpaid.ConsumptionBalanceHelper.Trio()
                        {
                            Code = item.CODIGO,
                            Description = item.DESCRIPCION
                        });
                    }
                }
            }

            objRechargesModel.Recharges = listRecharges;
            objRechargesModel.Services = listService;
            objRechargesModel.Service = objService;
            objRechargesModel.Title = objRechargesResponsePostPaid.Title;
            objRechargesModel.Telephone = objRechargesResponsePostPaid.Telephone;
            objRechargesModel.Message = objRechargesResponsePostPaid.Message;
            objRechargesModel.MessageRecharges = objRechargesResponsePostPaid.MessageRecharges;
            objRechargesModel.MessageRechargesM2M = objRechargesResponsePostPaid.MessageBalanceM2M;
            objRechargesModel.MessageRechargesM2MVisible = objRechargesResponsePostPaid.MessageBalanceM2MVisible;
            objRechargesModel.MessageRechargesVisible = objRechargesResponsePostPaid.MessageRechargesVisible;
            objRechargesModel.MessageVisible = objRechargesResponsePostPaid.MessageVisible;
            objRechargesModel.ColorStateLine = objRechargesResponsePostPaid.ForeColorStateLine;

            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
            try
            {
                string strText = "";
                if (listRecharges.Count > 0 || objRechargesResponsePostPaid.MessageRecharges == "Success")
                {
                    strText = Claro.SIACU.Constants.ReturnBalance;
                }
                else
                {
                    strText = Claro.SIACU.Constants.ReturnBalanceNull;
                }
                strText = strText + objRechargesRequestPostPaid.Telephone;
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, objRechargesRequestPostPaid.Telephone, KEY.AppSettings("strAudiTraCodConsumoSaldo"), strText); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
            }

            return Json(new { data = objRechargesModel });
        }
        public ActionResult ConsumptionBalanceHFCLTE()
        {
            return PartialView();
        }
        public JsonResult GetTelephoneType(string strIdSession, CommonService.TelephoneTypeRequestCommon objTelephoneTypeRequestCommon)
        {
            CommonService.TelephoneTypeResponseCommon objTelephoneTypeResponseCommon = null;

            try
            {
                objTelephoneTypeRequestCommon.audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
                objTelephoneTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.TelephoneTypeResponseCommon>(() => { return oServiceCommon.GetTelephoneType(objTelephoneTypeRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTelephoneTypeRequestCommon.audit.Session, objTelephoneTypeRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(objTelephoneTypeRequestCommon.audit.transaction);
            }

            Areas.Dashboard.Models.PostpaidDataService.ConsumptionBalanceHFCLTEModel objBalanceModel = new Models.PostpaidDataService.ConsumptionBalanceHFCLTEModel();

            if (objTelephoneTypeResponseCommon.TelephoneTypes != null)
            {
                List<Helpers.Postpaid.ConsumptionBalanceHFCLTEHelper.TelephoneType> listTelephoneTypes = new List<Helpers.Postpaid.ConsumptionBalanceHFCLTEHelper.TelephoneType>();

                foreach (CommonService.ListItem item in objTelephoneTypeResponseCommon.TelephoneTypes)
                {
                    listTelephoneTypes.Add(new Helpers.Postpaid.ConsumptionBalanceHFCLTEHelper.TelephoneType()
                    {
                        Description = item.Description
                    });
                }
                objBalanceModel.TelephoneTypes = listTelephoneTypes;
            }

            return Json(new { data = objBalanceModel });
        }

        //INICIATIVA 616 - INI
        public JsonResult GetBalanceFijaTobe(string strIdSession, string msisdn, string coIdPub)
        {
            List<Helpers.Postpaid.ConsumptionBalanceHFCLTEHelper.Balance> listBalances = new List<Helpers.Postpaid.ConsumptionBalanceHFCLTEHelper.Balance>();

            List<DashboardService.BalancePostPaid> objBalanceResponsePostPaid = new List<DashboardService.BalancePostPaid>();
            DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);

            try 
	        {	        
		        
                objBalanceResponsePostPaid = oService.GetBalanceFijaTobe(objAudit, msisdn, coIdPub);

                if(objBalanceResponsePostPaid != null &&
                   objBalanceResponsePostPaid.Count > 0)
                {
                    foreach (var item in objBalanceResponsePostPaid)
                    {
                        listBalances.Add(new Helpers.Postpaid.ConsumptionBalanceHFCLTEHelper.Balance()
                        {
                            WalletDescription = item.WALLET_DESCRIPTION,
                            Consumo = item.CONSUMO,
                            Balances = item.BALANCE
                        });
                    }
                }                
	        }
	        catch (Exception ex)
	        {
                Claro.Web.Logging.Error(strIdSession, objAudit.transaction, ex.Message);
                throw new Claro.MessageException(objAudit.transaction);
	        }

            Models.PostpaidDataService.ConsumptionBalanceHFCLTEModel objConsumptionBalanceHFCLTEModel = new Models.PostpaidDataService.ConsumptionBalanceHFCLTEModel()
            {
                Balances = listBalances
            };

            return Json(new { data = objConsumptionBalanceHFCLTEModel.Balances });
        }
        //INICIATIVA 616 - FIN


        public JsonResult GetBalance(string strIdSession, PostpaidService.BalanceRequestPostPaid objBalanceRequestPostPaid)
        {
            PostpaidService.BalanceResponsePostPaid objBalanceResponsePostPaid;

            try
            {
                objBalanceRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objBalanceResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.BalanceResponsePostPaid>(() => { return oServicepost.GetBalance(objBalanceRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objBalanceRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objBalanceRequestPostPaid.audit.transaction);
            }

            List<Helpers.Postpaid.ConsumptionBalanceHFCLTEHelper.Balance> listBalances = new List<Helpers.Postpaid.ConsumptionBalanceHFCLTEHelper.Balance>();

            if (objBalanceResponsePostPaid.Balances != null)
            {
                foreach (PostpaidService.BalancePostPaid item in objBalanceResponsePostPaid.Balances)
                {
                    listBalances.Add(new Helpers.Postpaid.ConsumptionBalanceHFCLTEHelper.Balance()
                    {
                        WalletDescription = item.WALLET_DESCRIPTION,
                        Consumo = item.CONSUMO,
                        Balances = item.BALANCE
                    });
                }
            }

            Models.PostpaidDataService.ConsumptionBalanceHFCLTEModel objConsumptionBalanceHFCLTEModel = new Models.PostpaidDataService.ConsumptionBalanceHFCLTEModel()
            {
                Balances = listBalances
            };
            return Json(new { data = objConsumptionBalanceHFCLTEModel.Balances });
        }
        #endregion

        public ActionResult PinPuk()
        {
            return View();
        }

        public ActionResult LineHistory(string strIdSession, string strContractID, string plataform, string flagConvivencia, string coIdPub)
        {
            Dashboard.Models.Postpaid.DataLineServiceModel objDataLineServiceModel = new Dashboard.Models.Postpaid.DataLineServiceModel();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            if (!strContractID.Equals(""))
            {
                try
                {
                    List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine = GetDataLineHistory(strContractID, plataform, audit, flagConvivencia, coIdPub);
                    objDataLineServiceModel = DataLineServiceHistoryModel(lstLine);

                    if (flagConvivencia == null)
                    {
                        objDataLineServiceModel.ContractID = strContractID;
                    }
                    else
                    {
                        objDataLineServiceModel.coIdPub = coIdPub;
                    }


                }
                catch (Exception ex)
                {

                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }

            }
            return PartialView(objDataLineServiceModel);

        }

        public  List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> GetDataLineHistory(string strContractID, string plataform, PostpaidService.AuditRequest audit, string flagConvivencia, string coIdPub)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid();

            objRequest.ContractID = strContractID;
            objRequest.audit = audit;
            objRequest.plataformaAT = plataform;
            objRequest.flagConvivencia = flagConvivencia;
            objRequest.coIdPub = coIdPub;

            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid>(() =>
            { return oServicepost.GetDataLineHistory(objRequest); });

            return objResponse.ListService;
        }

        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel DataLineServiceHistoryModel(List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstLine)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel objService = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataLineServiceModel();
            foreach (var item in lstLine)
            {
                objService.lstServices.Add(new Helpers.Postpaid.DataLineService.DataLineService()
                {
                    StateLine = item.ESTADO_LINEA,
                    Reason = item.MOTIVO,
                    Introduced = item.INTRODUCIDO_EL,
                    IntroducedBy = item.INTRODUCIDO_POR
                });
            }
            objService.lstServices = objService.lstServices.OrderByDescending(x => x.Introduced).ToList();
            return objService;
        }

        #region CONSULTA_4G HLR
        public ActionResult Consulta_4G(string strIdSession, string strPhoneNumber, string strICCID)
        {
            Dashboard.Models.PostpaidDataService.HLRModel objHLRModel = new Dashboard.Models.PostpaidDataService.HLRModel();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

            if (!strPhoneNumber.Equals(""))
            {
                try
                {

                    Claro.SIACU.Web.WebApplication.PostpaidService.HLRResponsePostPaid objResponse = GetHLR(strPhoneNumber, strIdSession);
                    objHLRModel = DataHLRModel(objResponse);
                    /*PROY-31249*/

                    if (!string.IsNullOrEmpty(strICCID))
                    {
                        objHLRModel = GetTechVo(audit, strPhoneNumber, strICCID, objHLRModel);
                    }
                    /*PROY-31249*/
                }
                catch (Exception ex)
                {

                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);

                }

                SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
                try
                {
                    string strConsultDate = DateTime.Now.ToString("yyyyMMdd HHmmss");
                    string strText = Claro.SIACU.Constants.Consult4G + strPhoneNumber + Claro.SIACU.Constants.DateAndTime + strConsultDate;
                    Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strPhoneNumber, KEY.AppSettings("strAudiTraCodConsulta4G"), strText); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
                    throw new Claro.MessageException(objaudit.transaction);
                }

            }

            return View(objHLRModel);
        }

        private Claro.SIACU.Web.WebApplication.PostpaidService.HLRResponsePostPaid GetHLR(string PhoneNumber, string strIdSession)
        {
            PostpaidService.HLRRequestPostPaid objRequest = new PostpaidService.HLRRequestPostPaid() { PhoneNumber = PhoneNumber, audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession) };
            Claro.SIACU.Web.WebApplication.PostpaidService.HLRResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.HLRResponsePostPaid>(() =>
            {
                return oServicepost.GetHLR(objRequest);
            });
            return objResponse;

        }

        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.HLRModel DataHLRModel(PostpaidService.HLRResponsePostPaid response)
        {

            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.HLRModel objHLR = new Models.PostpaidDataService.HLRModel();
            objHLR.strTechnology4G = response.Tecnologia4G;
            if (response.Tecnologia4G.Equals(""))
            {
                objHLR.strTechnology4G = response.ErrorMessage;
            }

            return objHLR;
        }

        #endregion

        #region Servicios VoLTE y VoWiFi


        private Dashboard.Models.PostpaidDataService.HLRModel GetTechVo(PostpaidService.AuditRequest oAudit, string PhoneNumber, string NroICCID, Dashboard.Models.PostpaidDataService.HLRModel objHLRModel)
        {
            try
            {
                Claro.SIACU.Web.WebApplication.PostpaidService.StatusTechnologyVo oStatusTechnologyVo = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.StatusTechnologyVo>(() =>
                {
                    return oServicepost.GetStatusTechnologyVo(oAudit, NroICCID, PhoneNumber);
                });

                if (oStatusTechnologyVo != null)
                {
                    objHLRModel.strTechnologyVoLTE = oStatusTechnologyVo.TechnologyVoLte;
                    objHLRModel.strTechnologyVoWifi = oStatusTechnologyVo.TechnologyVoWifi;

                    if (!string.IsNullOrEmpty(oStatusTechnologyVo.MessageError) && !string.IsNullOrEmpty(objHLRModel.strMessageError))
                    {
                        objHLRModel.strMessageError = oStatusTechnologyVo.MessageError;
                    }

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oAudit.Session, oAudit.transaction, ex.Message);
            }

            return objHLRModel;

        }

        #endregion

        #region [Prestamo/alquiler]

        public ActionResult LoanRental()
        {
            return PartialView();
        }



        public JsonResult getLoanRentalType(string strIdSession)
        {

            PostpaidService.LoanRentalResponseCommon ObjLoanRentalResponseCommon = null;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            PostpaidService.LoanRentalRequestCommon objLoanRentalRequestCommon = new PostpaidService.LoanRentalRequestCommon()
            {
                audit = audit,
            };
            try
            {

                ObjLoanRentalResponseCommon = Claro.Web.Logging.ExecuteMethod<PostpaidService.LoanRentalResponseCommon>(() => { return oServicepost.GetLoanRentalType(objLoanRentalRequestCommon); });
            }
            catch (Exception ex)
            {
                ObjLoanRentalResponseCommon = null;
                Claro.Web.Logging.Error(strIdSession, objLoanRentalRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.LoandRentalType ObjLoanRentalType = null;
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.LoandRentalType> ListLoanRentalType = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.LoandRentalType>();
            Areas.Dashboard.Models.PostpaidDataService.LoanRentalModel ObjLoanRentalModel = new Areas.Dashboard.Models.PostpaidDataService.LoanRentalModel();

            if (ObjLoanRentalResponseCommon != null)
            {
                foreach (PostpaidService.ListItem item in ObjLoanRentalResponseCommon.ListItem)
                {
                    ObjLoanRentalType = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.LoandRentalType();
                    ObjLoanRentalType.Code = item.Code;
                    ObjLoanRentalType.Description = item.Description;
                    ListLoanRentalType.Add(ObjLoanRentalType);
                }
                ObjLoanRentalModel.listLoanRentalType = ListLoanRentalType;
            }

            return Json(new { data = ObjLoanRentalModel.listLoanRentalType });
        }

        public JsonResult GetLoanRentalListwarehouseAreaType(string strIdSession)
        {
            PostpaidService.LoanRentalResponseCommon ObjLLoanRentalListWarehouseAreaResponseCommon = null;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            PostpaidService.LoanRentalRequestCommon objLoanRentalRequestCommon = new PostpaidService.LoanRentalRequestCommon()
            {
                audit = audit,
            };

            try
            {

                ObjLLoanRentalListWarehouseAreaResponseCommon = Claro.Web.Logging.ExecuteMethod<PostpaidService.LoanRentalResponseCommon>(() => { return oServicepost.GetLoanRentalListWarehouseArea(objLoanRentalRequestCommon); });
            }
            catch (Exception ex)
            {
                ObjLLoanRentalListWarehouseAreaResponseCommon = null;
                Claro.Web.Logging.Error(strIdSession, objLoanRentalRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.LoandRentalType ObjLoanRentalType = null;
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.LoandRentalType> ListtLoanRentalListWarehouseAreaType = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.LoandRentalType>();
            Areas.Dashboard.Models.PostpaidDataService.LoanRentalModel ObjListtLoanRentalListWarehouseAreaTypelModel = new Areas.Dashboard.Models.PostpaidDataService.LoanRentalModel();

            if (ObjLLoanRentalListWarehouseAreaResponseCommon != null)
            {
                foreach (PostpaidService.ListItem item in ObjLLoanRentalListWarehouseAreaResponseCommon.ListItem)
                {
                    ObjLoanRentalType = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.LoandRentalType();
                    ObjLoanRentalType.Code = item.Code;
                    ObjLoanRentalType.Description = item.Description;
                    ListtLoanRentalListWarehouseAreaType.Add(ObjLoanRentalType);
                }
                ObjListtLoanRentalListWarehouseAreaTypelModel.listLoanRentalType = ListtLoanRentalListWarehouseAreaType;
            }

            return Json(new { data = ObjListtLoanRentalListWarehouseAreaTypelModel.listLoanRentalType });
        }

        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.LoanRentalModel LoanRentals(string strIdSession, string strStarDate, string strEndDate, string strNumber, string strModel, string StrNroDocumento)
        {

            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.LoanRental> listLoanRental2 = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.LoanRental>();
            PostpaidService.LoanRentalResponsePostPaid oLoanRentalResponse = new PostpaidService.LoanRentalResponsePostPaid();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            PostpaidService.LoanRentalRequestPostPaid oLoanRentalRequest = new PostpaidService.LoanRentalRequestPostPaid()
            {
                audit = audit
            };
            oLoanRentalRequest.StarDate = strStarDate;
            oLoanRentalRequest.EndDate = strEndDate;
            oLoanRentalRequest.Number = strNumber;
            oLoanRentalRequest.Model = strModel;
            oLoanRentalRequest.NroDocumento = StrNroDocumento;

            try
            {
                oLoanRentalResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.LoanRentalResponsePostPaid>(() =>
                { return oServicepost.LoanRental(oLoanRentalRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, oLoanRentalRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            if (oLoanRentalResponse != null)
            {
                foreach (var item in oLoanRentalResponse.ObjLoanRental.lstCabAccounLoanRental)
                {


                    listLoanRental2.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.LoanRental()
                    {
                        Businessname = item.RAZON_SOCIAL,
                        RUC = item.RUC,
                        address = item.DIRECCION,
                        IMEI = item.IMEI,
                        reasonSAP = item.MOTIVO_SAP,
                        modalitySAP = item.MODALIDAD_SAP,
                        Date = item.FECHAS,
                        NroClarify = item.NUMERO_CLARIFY,
                        NroOrder = item.NUMERO_PEDIDO,
                        Denomination = item.DENOMINACION,
                        networth = item.VALOR_NETO,
                        Model = item.MODELO
                    });

                }
            }

            Areas.Dashboard.Models.PostpaidDataService.LoanRentalModel oLoanRentalModel = new Areas.Dashboard.Models.PostpaidDataService.LoanRentalModel()
            {
                listLoanRental = listLoanRental2
            };


            return oLoanRentalModel;
        }

        public JsonResult SearchLoanRental(string strIdSession, string strStarDate, string strEndDate, string strNumber, string strModel, string StrNroDocumento)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.LoanRentalModel objLoanRentalModel = LoanRentals(strIdSession, strStarDate, strEndDate, strNumber, strModel, StrNroDocumento);
            return Json(new { data = objLoanRentalModel.listLoanRental });
        }


        public JsonResult ExportLoanRental(string strIdSession, string strStarDate, string strEndDate, string strNumber, string strModel, string StrNroDocumento)
        {
            List<Helpers.Postpaid.LoanRental.LoanRentalNoClarify> listLoanRentalNoClarify2 = new List<Helpers.Postpaid.LoanRental.LoanRentalNoClarify>();
            List<Helpers.Postpaid.LoanRental.LoanRental> listLoanRental2 = new List<Helpers.Postpaid.LoanRental.LoanRental>();
            Models.PostpaidDataService.LoanRentalModel objLoanRentalModel = LoanRentals(strIdSession, strStarDate, strEndDate, strNumber, strModel, StrNroDocumento);
            if (objLoanRentalModel == null)
            {
                objLoanRentalModel = new Models.PostpaidDataService.LoanRentalModel();
            }
            objLoanRentalModel.DateFin = strEndDate;
            objLoanRentalModel.DateIni = strStarDate;
            objLoanRentalModel.Type = strModel;
            objLoanRentalModel.Number = StrNroDocumento;
            if (strModel != "IMEI")
            {
                objLoanRentalModel.listLoanRentalNoClarify.Clear();
                objLoanRentalModel.listLoanRentalNoClarify = null;
            }
            else
            {
                Helpers.Postpaid.LoanRental.LoanRentalNoClarify objLoanRentalNoClarify;
                objLoanRentalModel.listLoanRentalNoClarify = new List<Helpers.Postpaid.LoanRental.LoanRentalNoClarify>();
                foreach (var item in objLoanRentalModel.listLoanRental)
                {
                    objLoanRentalNoClarify = new Helpers.Postpaid.LoanRental.LoanRentalNoClarify();
                    objLoanRentalNoClarify.Businessname = item.Businessname;
                    objLoanRentalNoClarify.RUC = item.RUC;
                    objLoanRentalNoClarify.address = item.address;
                    objLoanRentalNoClarify.Model = item.Model;
                    objLoanRentalNoClarify.IMEI = item.IMEI;
                    objLoanRentalNoClarify.reasonSAP = item.reasonSAP;
                    objLoanRentalNoClarify.modalitySAP = item.modalitySAP;
                    objLoanRentalNoClarify.Date = item.Date;
                    objLoanRentalNoClarify.NroOrder = item.NroOrder;
                    objLoanRentalNoClarify.Denomination = item.Denomination;
                    objLoanRentalNoClarify.networth = item.networth;
                    objLoanRentalModel.listLoanRentalNoClarify.Add(objLoanRentalNoClarify);
                }
                objLoanRentalModel.listLoanRental.Clear();
                objLoanRentalModel.listLoanRental = null;
            }
            string path = oExcelHelper.ExportExcel(objLoanRentalModel, TemplateExcel.CONST_LOA_REN);
            return Json(path);
        }
        #endregion

        #region [acuerdoLinea]

        public JsonResult PostLineAgreement()
        {
            string strRuta = "";
            string strSistema = "";

            strRuta = KEY.AppSettings("strRutaSiteSIGA");
            strSistema = KEY.AppSettings("strSistemaSIACPNS");

            return Json(new { DataRuta = strRuta, DataSystem = strSistema });
        }
        #endregion

        public ActionResult HistoryActions()
        {
            return PartialView();
        }
        public JsonResult GetHistoryActions(string strIdSession, string flagConvivencia, string plataform, PostpaidService.HistoryActionsRequestPostPaid objHistoryActionsRequestPostPaid)
        {
            PostpaidService.HistoryActionsResponsePostPaid objHistoryActionsResponsePostPaid = null;

            try
            {
                objHistoryActionsRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objHistoryActionsResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.HistoryActionsResponsePostPaid>(() => { return oServicepost.GetHistoryActions(objHistoryActionsRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objHistoryActionsRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objHistoryActionsRequestPostPaid.audit.transaction);
            }

            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.HistoryActions.HistoryActions objHistAct = null;
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.HistoryActions.HistoryActions> listHistoryActions = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.HistoryActions.HistoryActions>();
            Areas.Dashboard.Models.PostpaidDataService.HistoryActionsModel objHistoryActionsModel = new Areas.Dashboard.Models.PostpaidDataService.HistoryActionsModel();

            if (objHistoryActionsResponsePostPaid != null)
            {
                foreach (PostpaidService.HistoryActionsPostPaid item in objHistoryActionsResponsePostPaid.ListHistoryActions)
                {
                    objHistAct = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.HistoryActions.HistoryActions();
                    objHistAct.Contract = item.CONTRATO;
                    objHistAct.Description = item.DESCRIPCION;
                    objHistAct.DateOrder = item.FECH_ORDE;
                    objHistAct.Date = item.FECHA;
                    objHistAct.Hour = item.HORA;
                    objHistAct.Service = item.SERVICIO;
                    objHistAct.User = item.USUARIO;
                    listHistoryActions.Add(objHistAct);
                }
                objHistoryActionsModel.listHistoryActions = listHistoryActions;
            }

            return Json(new { data = objHistoryActionsModel.listHistoryActions });
        }
        public ActionResult RegRoaming()
        {
            return PartialView();
        }

        public ActionResult PortabilityConsultation(string strIdSession, string strTelephone, string strTextDate)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.PortabilityModel objPortabilityModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.PortabilityModel();
            Claro.SIACU.Web.WebApplication.CommonService.PortabilityResponseCommon objPortability;
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            if (!strTelephone.Equals(""))
            {
                try
                {
                    objPortability = GetPortability(strTelephone, audit);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }
                objPortabilityModel = DataPortabilityModel(objPortability, strTelephone);
                objPortabilityModel.TextDate = strTextDate;
            }


            return PartialView(objPortabilityModel);
        }

        private Claro.SIACU.Web.WebApplication.CommonService.PortabilityResponseCommon GetPortability(string strTelephone, CommonService.AuditRequest audit)
        {


            Claro.SIACU.Web.WebApplication.CommonService.PortabilityRequestCommon objRequest = new CommonService.PortabilityRequestCommon();
            objRequest.Telephone = strTelephone;
            objRequest.audit = audit;
            Claro.SIACU.Web.WebApplication.CommonService.PortabilityResponseCommon objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.CommonService.PortabilityResponseCommon>(() =>
            { return oServiceCommon.GetPortability(objRequest); });

            return objResponse;
        }

        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.PortabilityModel DataPortabilityModel(Claro.SIACU.Web.WebApplication.CommonService.PortabilityResponseCommon objPortability, string strTelephone)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.PortabilityModel objPortabilityModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService.PortabilityModel();

            if (objPortability.ListPortability != null && objPortability.ListPortability.Count > 0)
            {
                objPortabilityModel.NumberRequest = objPortability.ListPortability[0].NUMERO_SOLICITUD;
                objPortabilityModel.ProcessStatus = objPortability.ListPortability[0].ESTADO_PROCESO;
                objPortabilityModel.RegistrationDate = objPortability.ListPortability[0].FECHA_REGISTRO;
                objPortabilityModel.ExecutionDate = objPortability.ListPortability[0].FECHA_EJECUCION;
                objPortabilityModel.Operator = objPortability.Operator;
                objPortabilityModel.Answer = objPortability.Respuesta;
                objPortabilityModel.TypeProcessDate = objPortability.TypeProcessDate;
                objPortabilityModel.TypeProcessOperator = objPortability.TypeProcessOperator;
                objPortabilityModel.ExecutionDate = objPortability.ExecutionDate;
                objPortabilityModel.DescriptionProcessStatus = objPortability.ListPortability[0].DESCRPCION_ESTADO_PROCESO;
            }

            return objPortabilityModel;
        }

        public JsonResult SearchStock(string strIdSession, string strSeries, string strDescription, string cbowarehouseAreaType)
        {
            PostpaidService.StockWarehouseResponseDashboard oStockWarehouseResponse = new PostpaidService.StockWarehouseResponseDashboard();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            PostpaidService.StockWarehouseRequestDashboard oStockWarehouseRequest = new PostpaidService.StockWarehouseRequestDashboard()
            {
                audit = audit,
            };
            Areas.Dashboard.Models.PostpaidDataService.StockWarehouseModal objStockWarehouseModal = new Models.PostpaidDataService.StockWarehouseModal();
            oStockWarehouseRequest.strSerie = strSeries;
            oStockWarehouseRequest.strDescripcion = strDescription;
            oStockWarehouseRequest.strRegion = cbowarehouseAreaType;
            try
            {
                oStockWarehouseResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.StockWarehouseResponseDashboard>(() =>
                { return oServicepost.GetStockWarehouse(oStockWarehouseRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, oStockWarehouseRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }
            if (oStockWarehouseResponse != null)
            {
                foreach (var item in oStockWarehouseResponse.listStockWarehouse)
                {
                    objStockWarehouseModal.listStockWarehouse.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.StockWarehouse()
                    {
                        Codigo = item.Codigo,
                        Descripcion = item.Descripcion,
                        Warehouse = item.Warehouse,
                        Quantity = item.Quantity
                    });

                }
            }

            return Json(new { data = objStockWarehouseModal });
        }

        public ActionResult ExamineStock()
        {
            return PartialView();
        }

        public JsonResult GetMaterials(string strIdSession)
        {
            PostpaidService.MaterialsResponsePostpaid objMaterialsResponseDashboard = new PostpaidService.MaterialsResponsePostpaid();
            PostpaidService.MaterialsRequestPostpaid objMaterialsRequestDashboard = new PostpaidService.MaterialsRequestPostpaid();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            objMaterialsRequestDashboard.audit = audit;
            try
            {
                objMaterialsResponseDashboard = Claro.Web.Logging.ExecuteMethod<PostpaidService.MaterialsResponsePostpaid>(() => { return oServicepost.GetMaterials(objMaterialsRequestDashboard); });
            }
            catch (Exception ex)
            {
                objMaterialsResponseDashboard = null;
                Claro.Web.Logging.Error(strIdSession, objMaterialsRequestDashboard.audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.Materials objMaterials = null;

            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.Materials> listMaterials = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.Materials>();

            Areas.Dashboard.Models.PostpaidDataService.Materials objModelMaterials = new Models.PostpaidDataService.Materials();
            if (objMaterialsResponseDashboard != null)
            {
                foreach (var item in objMaterialsResponseDashboard.ListMateriales)
                {
                    objMaterials = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental.Materials();
                    objMaterials.Id = item.Id;
                    objMaterials.Description = item.Description;
                    listMaterials.Add(objMaterials);

                }
                objModelMaterials.ListMaterials = listMaterials;
            }

            JsonResult result = Json(new { data = objModelMaterials.ListMaterials });
            result.MaxJsonLength = int.MaxValue;

            return result;
        }

        public ActionResult GetBalanceCBIOS()
        {
            return PartialView();
        }

        public JsonResult GetBalancesCBIOS(string strIdSession, string strCustomerId, string strTelephone)
        {


            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS.BalanceCBIOS> lstBolsaIndividual = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS.BalanceCBIOS>();
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS.BalanceCBIOS> lstBolsaOnTop = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS.BalanceCBIOS>();
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS.BalanceCBIOS> lstBolsaCorporativa = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS.BalanceCBIOS>();
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS.BalanceCBIOS objMaterials = null;
            PostpaidService.BalanceCBIOSResponsePostPaid objMaterialsResponseDashboard = new PostpaidService.BalanceCBIOSResponsePostPaid();
            PostpaidService.BalanceCBIOSRequestPostPaid objMaterialsRequestDashboard = new PostpaidService.BalanceCBIOSRequestPostPaid();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            objMaterialsRequestDashboard.audit = audit;
            objMaterialsRequestDashboard.CustomerId = strCustomerId;
            objMaterialsRequestDashboard.Host = ConfigurationManager.AppSettings("strHostTransaction").ToString();
            objMaterialsRequestDashboard.Telephone = "51" + strTelephone;
            try
            {
                objMaterialsResponseDashboard = Claro.Web.Logging.ExecuteMethod<PostpaidService.BalanceCBIOSResponsePostPaid>(() => { return oServicepost.GetBalanceCBIOS(objMaterialsRequestDashboard); });
            }
            catch (Exception ex)
            {
                objMaterialsResponseDashboard = null;
                Claro.Web.Logging.Error(strIdSession, objMaterialsRequestDashboard.audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }


            Areas.Dashboard.Models.PostpaidDataService.BalanceCBIOS objModelMaterials = new Models.PostpaidDataService.BalanceCBIOS();
            if (objMaterialsResponseDashboard != null)
            {
                foreach (var item in objMaterialsResponseDashboard.lstBagBalanceCBIOS)
                {

                    objMaterials = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS.BalanceCBIOS();
                    objMaterials.CodigoConsumo = item.CodigoConsumo;
                    objMaterials.Bolsa = item.Bolsa;
                    objMaterials.Saldo = item.Saldo;
                    objMaterials.Consumo = item.Consumo;
                    objMaterials.UnidadesAsignadas = item.Consumo + item.Saldo;

                    if (item.SharedProduct.Equals("false"))
                    {
                        if (item.Type.Equals("B"))
                        {
                            lstBolsaIndividual.Add(objMaterials);
                        }
                        if (item.Type.Equals("O"))
                        {
                            lstBolsaOnTop.Add(objMaterials);
                        }
                    }
                    else
                    {
                        objMaterials.lineLimit = item.lineLimit;
                        objMaterials.lineConsumption = item.lineConsumption;
                        objMaterials.LimiteDisponible = item.lineLimit - item.lineConsumption;
                        lstBolsaCorporativa.Add(objMaterials);
                    }


                }
                if (objMaterialsResponseDashboard.strResponseCode.Equals("1"))
                {
                    objModelMaterials.lstBolsaCorporativa = lstBolsaCorporativa;
                    objModelMaterials.lstBolsaOnTop = lstBolsaOnTop;
                    objModelMaterials.lstBolsaIndividual = lstBolsaIndividual;
                    objModelMaterials.Message = objMaterialsResponseDashboard.strResponseCode;
                }
                else if (objMaterialsResponseDashboard.strResponseCode.Equals("-1") || objMaterialsResponseDashboard.strResponseCode.Equals("0"))
                {
                    objModelMaterials.Message = objMaterialsResponseDashboard.strResponseCode;
                }

            }
            else
            {
                objModelMaterials.Message = "-1";
            }

            JsonResult result = Json(new { data = objModelMaterials });
            result.MaxJsonLength = int.MaxValue;

            return result;
        }
        public ActionResult GetHistoryPackage()
        {
            return PartialView();
        }
        public ActionResult GetHistoryRecharge()
        {
            return PartialView();
        }
        public ActionResult GetQueryBalanceShared()
        {
            return PartialView();
        }
        public ActionResult GetConsumptionHistoricalRecharge()
        {
            return PartialView();
        }

        public JsonResult GetJsonBalanceShared(string strIdSession, PostpaidService.SharedBagRequestPostPaid objSharedBagRequestPostPaid)
        {
            PostpaidService.SharedBagResponsePostPaid objGetSharedBagResponse = new PostpaidService.SharedBagResponsePostPaid();

            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBag> lstSharedBag = null;
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBagLine> lstSharedBagLine = null;


            objSharedBagRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            Areas.Dashboard.Models.PostpaidDataAccount.AccountSharedBagModel oPostDataSharedBagModel = new Areas.Dashboard.Models.PostpaidDataAccount.AccountSharedBagModel()
            {
                Account = objSharedBagRequestPostPaid.Account,
                Line = objSharedBagRequestPostPaid.Telephone,
                lstSharedBag = null,
                lstSharedBagLine = null
            };
            try
            {
                objGetSharedBagResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.SharedBagResponsePostPaid>(
                    () => { return oServicepost.GetDataBalanceShared(objSharedBagRequestPostPaid); });


                if (objGetSharedBagResponse.ListSharedBag != null && objGetSharedBagResponse.ListSharedBag.Count > 0)
                {
                    lstSharedBag = new List<HELPER.AccountSharedBag.AccountSharedBag>();
                    foreach (PostpaidService.SharedBagPostPaid item in objGetSharedBagResponse.ListSharedBag)
                    {
                        lstSharedBag.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBag()
                        {
                            Account = item.CUENTA,
                            Bag = item.BOLSA,
                            Line = item.LINEA,
                            Unit = item.UNIDAD,
                            Stopper = item.TOPE,
                            Consumption = item.CONSUMO,
                            Balance = item.SALDO,
                            DateValidity = item.FECHAVIGENCIA,
                            Optional1 = item.OPCIONAL1,
                            Optional2 = item.OPCIONAL2,
                            GroupBag = item.GRUPO_BOLSA
                        });
                    }
                }
                if (objGetSharedBagResponse.ListSharedBagLine != null && objGetSharedBagResponse.ListSharedBagLine.Count > 0)
                {
                    lstSharedBagLine = new List<HELPER.AccountSharedBag.AccountSharedBagLine>();
                    foreach (PostpaidService.SharedBagPostPaid item in objGetSharedBagResponse.ListSharedBagLine)
                    {
                        lstSharedBagLine.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBagLine()
                        {
                            Account = item.CUENTA,
                            Bag = item.BOLSA,
                            Line = item.LINEA,
                            Unit = item.UNIDAD,
                            Stopper = item.TOPE,
                            Consumption = item.CONSUMO,
                            Balance = item.SALDO,
                            DateValidity = item.FECHAVIGENCIA,


                        });
                    }
                }


            }
            catch (Exception ex)
            {
                lstSharedBag = null;
                lstSharedBagLine = null;

                oPostDataSharedBagModel.Error = KEY.AppSettings("strMsgErrorTransaccion");
                Claro.Web.Logging.Error(strIdSession, objSharedBagRequestPostPaid.audit.transaction, ex.Message);
            }




            oPostDataSharedBagModel.lstSharedBag = lstSharedBag;
            oPostDataSharedBagModel.lstSharedBagLine = lstSharedBagLine;
            oPostDataSharedBagModel.Error = objGetSharedBagResponse.Message;
            oPostDataSharedBagModel.MessageTypeCustomer = objGetSharedBagResponse.MessageTypeCustomer;
            oPostDataSharedBagModel.CountSharedBag = objGetSharedBagResponse.CountSharedBag;




            return Json(new { data = oPostDataSharedBagModel });
        }

        public JsonResult GetJsonHistoryRecharge(string strIdSession, PostpaidService.HistoricalRechargeRequestPostPaid objSharedBagRequestPostPaid)
        {
            PostpaidService.HistoricalRechargeResponsePostpaid objHistoricalRechargeResponsePostpaid = new PostpaidService.HistoricalRechargeResponsePostpaid();
            List<HELPER.HistoricalRecharge.HistoricalRechargeHelper> lstHistoricalRechargeHelper = null;


            objSharedBagRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

            try
            {
                objHistoricalRechargeResponsePostpaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.HistoricalRechargeResponsePostpaid>(
                    () => { return oServicepost.GetHistoricalRecharge(objSharedBagRequestPostPaid); });


                if (objHistoricalRechargeResponsePostpaid.lstHistoricalRecharge != null && objHistoricalRechargeResponsePostpaid.lstHistoricalRecharge.Count > 0)
                {
                    lstHistoricalRechargeHelper = new List<HELPER.HistoricalRecharge.HistoricalRechargeHelper>();
                    foreach (PostpaidService.HistoricalRechargePostPaid item in objHistoricalRechargeResponsePostpaid.lstHistoricalRecharge)
                    {
                        lstHistoricalRechargeHelper.Add(new HELPER.HistoricalRecharge.HistoricalRechargeHelper()
                        {
                            amountRecharge = item.AMOUNT_RECHARGE,
                            canal = item.CANAL,
                            dateRecharge = item.DATE_RECHARGE

                        });
                    }
                }



            }
            catch (Exception ex)
            {
                lstHistoricalRechargeHelper = null;
                Claro.Web.Logging.Error(strIdSession, objSharedBagRequestPostPaid.audit.transaction, ex.Message);
            }

            Areas.Dashboard.Models.PostpaidDataService.HistoricalRechargeModel objHistoricalRechargeModel = new Areas.Dashboard.Models.PostpaidDataService.HistoricalRechargeModel()
            {
                lstHistoricalRechargeHelper = lstHistoricalRechargeHelper,
                MessageAlert = objHistoricalRechargeResponsePostpaid.strMensajeAlert
            };





            return Json(new { data = objHistoricalRechargeModel });
        }


        public JsonResult GetTypeConsumption(string strIdSession, PostpaidService.GetTypeConsumptionRequestCommon objGetTypeConsumptionRequestCommon)
        {
            PostpaidService.GetTypeConsumptionResponseCommon objGetTypeConsumptionResponseCommon = new PostpaidService.GetTypeConsumptionResponseCommon();
            objGetTypeConsumptionRequestCommon.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            try
            {
                objGetTypeConsumptionResponseCommon = Claro.Web.Logging.ExecuteMethod<PostpaidService.GetTypeConsumptionResponseCommon>(() => { return oServicepost.GetTypeConsumption(objGetTypeConsumptionRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetTypeConsumptionRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetTypeConsumptionRequestCommon.audit.transaction);
            }

            ConsumptionRechargeModel objConsumptionRechargeModel = new ConsumptionRechargeModel();

            if (objGetTypeConsumptionResponseCommon.ListItem != null)
            {
                List<HELPER.ConsumptionType.ConsumptionTypeHelper> lstConsumptionTypeHelper = new List<HELPER.ConsumptionType.ConsumptionTypeHelper>();

                foreach (PostpaidService.ListItem item in objGetTypeConsumptionResponseCommon.ListItem)
                {
                    lstConsumptionTypeHelper.Add(new HELPER.ConsumptionType.ConsumptionTypeHelper()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }

                objConsumptionRechargeModel.lstConsumptionTypeHelper = lstConsumptionTypeHelper;
            }

            return Json(new { data = objConsumptionRechargeModel });


        }
        public JsonResult GetJsonConsumptionRecharge(string strIdSession, PostpaidService.ConsumptionHistoricalRechargeRequestPospaid objConsumptionHistoricalRechargeRequestPospaid)
        {

            PostpaidService.ConsumptionHistoricalRechargeResponsePospaid objConsumptionHistoricalRechargeResponsePospaid = new PostpaidService.ConsumptionHistoricalRechargeResponsePospaid();
            List<HELPER.ConsumptionRecharge.ConsumptionRechargeHelper> lstConsumptionRechargeHelper = null;


            objConsumptionHistoricalRechargeRequestPospaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

            try
            {
                objConsumptionHistoricalRechargeResponsePospaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.ConsumptionHistoricalRechargeResponsePospaid>(
                    () => { return oServicepost.GetConsumptionHistoricalRecharge(objConsumptionHistoricalRechargeRequestPospaid); });


                if (objConsumptionHistoricalRechargeResponsePospaid.lstConsumptionRecharge != null && objConsumptionHistoricalRechargeResponsePospaid.lstConsumptionRecharge.Count > 0)
                {
                    lstConsumptionRechargeHelper = new List<HELPER.ConsumptionRecharge.ConsumptionRechargeHelper>();
                    foreach (PostpaidService.ConsumptionRechargePostPaid item in objConsumptionHistoricalRechargeResponsePospaid.lstConsumptionRecharge)
                    {
                        lstConsumptionRechargeHelper.Add(new HELPER.ConsumptionRecharge.ConsumptionRechargeHelper()
                        {
                            AmountConsumed = item.AmountConsumed,
                            Balance = item.Balance,
                            DateEvent = item.DateEvent,
                            IdBag = item.IdBag,
                            NumberDestinationAPN = item.NumberDestinationAPN,
                            TypeConsumption = item.TypeConsumption
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                lstConsumptionRechargeHelper = null;
                Claro.Web.Logging.Error(strIdSession, objConsumptionHistoricalRechargeRequestPospaid.audit.transaction, ex.Message);
            }

            Areas.Dashboard.Models.PostpaidDataService.ConsumptionRechargeModel objConsumptionRechargeModel = new Areas.Dashboard.Models.PostpaidDataService.ConsumptionRechargeModel()
            {
                lstConsumptionRechargeHelper = lstConsumptionRechargeHelper,
                lstConsumptionTypeHelper = null,
                strMensajeAlert = objConsumptionHistoricalRechargeResponsePospaid.strMensajeAlert
            };


            return Json(new { data = objConsumptionRechargeModel });
        }



        public JsonResult GetJsonMBTotal(string strIdSession, PostpaidService.TotalMbPurchasedPackageRequestPospaid objTotalMbPurchasedPackageRequestPospaid)
        {

            PostpaidService.TotalMbPurchasedPackageResponsePospaid objTotalMbPurchasedPackageResponsePospaid = new PostpaidService.TotalMbPurchasedPackageResponsePospaid();

            HELPER.GetMbTotal.MbTotalHelper objMbTotalHelper = new HELPER.GetMbTotal.MbTotalHelper();

            objTotalMbPurchasedPackageRequestPospaid.objAudit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);


            try
            {
                objTotalMbPurchasedPackageResponsePospaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.TotalMbPurchasedPackageResponsePospaid>(
                    () => { return oServicepost.GetTotalMbPurchasedPackageResponse(objTotalMbPurchasedPackageRequestPospaid); });

                objMbTotalHelper.TotalMBCicle = objTotalMbPurchasedPackageResponsePospaid.TotalMBCicle;

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, objTotalMbPurchasedPackageRequestPospaid.audit.transaction, ex.Message);
            }

            Areas.Dashboard.Models.PostpaidDataService.HistoricalPackageModel objHistoricalPackageModel = new Areas.Dashboard.Models.PostpaidDataService.HistoricalPackageModel()
            {
                objMbTotalHelper = objMbTotalHelper
            };


            return Json(new { data = objHistoricalPackageModel });
        }
        public JsonResult GetJsonMBAvailable(string strIdSession, PostpaidService.MbBagRequestPospaid objMbBagRequestPospaid)
        {

            PostpaidService.MbBagResponsePospaid objMbBagResponsePospaid = new PostpaidService.MbBagResponsePospaid();

            HELPER.GetMbAvailable.MbAvailableHelper objMbAvailableHelper = new HELPER.GetMbAvailable.MbAvailableHelper();

            objMbBagRequestPospaid.objAudit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);


            try
            {
                objMbBagResponsePospaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.MbBagResponsePospaid>(
                    () => { return oServicepost.GetMbBag(objMbBagRequestPospaid); });

                objMbAvailableHelper.strMbAvailable = objMbBagResponsePospaid.strMbAvailable;


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, objMbBagRequestPospaid.audit.transaction, ex.Message);
            }

            Areas.Dashboard.Models.PostpaidDataService.HistoricalPackageModel objHistoricalPackageModel = new Areas.Dashboard.Models.PostpaidDataService.HistoricalPackageModel()
            {
                objMbAvailableHelper = objMbAvailableHelper
            };


            return Json(new { data = objHistoricalPackageModel });
        }


        public JsonResult GetJsonTypePackage(string strIdSession, PostpaidService.TypePakageRequestCommon objTypePakageRequestCommon)
        {
            PostpaidService.TypePakageResponseCommon objTypePakageResponseCommon = new PostpaidService.TypePakageResponseCommon();
            objTypePakageRequestCommon.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            try
            {
                objTypePakageResponseCommon = Claro.Web.Logging.ExecuteMethod<PostpaidService.TypePakageResponseCommon>(() => { return oServicepost.GetTypePakage(objTypePakageRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objTypePakageRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(objTypePakageRequestCommon.audit.transaction);
            }

            ConsumptionRechargeModel objConsumptionRechargeModel = new ConsumptionRechargeModel();

            if (objTypePakageResponseCommon.ListItem != null)
            {
                List<HELPER.ConsumptionType.ConsumptionTypeHelper> lstTypePakageResponseHelper = new List<HELPER.ConsumptionType.ConsumptionTypeHelper>();

                foreach (PostpaidService.ListItem item in objTypePakageResponseCommon.ListItem)
                {
                    lstTypePakageResponseHelper.Add(new HELPER.ConsumptionType.ConsumptionTypeHelper()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }

                objConsumptionRechargeModel.lstConsumptionTypeHelper = lstTypePakageResponseHelper;
            }

            return Json(new { data = objConsumptionRechargeModel });


        }

        //vtorremo
        public JsonResult GetJsonHistoricalPackage(string strIdSession, PostpaidService.TotalMbPurchasedPackageRequestPospaid objTotalMbPurchasedPackageRequestPospaid, string plataforma,string flagConvivencia)
        {

            PostpaidService.TotalMbPurchasedPackageResponsePospaid objTotalMbPurchasedPackageResponsePospaid = new PostpaidService.TotalMbPurchasedPackageResponsePospaid();

            HELPER.GetHistoricalPackage.HistoricalPackageHelper objHistoricalPackageHelper = new HELPER.GetHistoricalPackage.HistoricalPackageHelper();
            List<HELPER.GetHistoricalPackage.PackageODCSHelper> lstPackageODCSHelper = null;
            objTotalMbPurchasedPackageRequestPospaid.objAudit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);


            try
            {

                objTotalMbPurchasedPackageResponsePospaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.TotalMbPurchasedPackageResponsePospaid>(
                                   () => { return oServicepost.GetHistoricalPackage(objTotalMbPurchasedPackageRequestPospaid, plataforma, flagConvivencia); });

                if (objTotalMbPurchasedPackageResponsePospaid.lstPackageODCS != null && objTotalMbPurchasedPackageResponsePospaid.lstPackageODCS.Count > 0)
                {
                    lstPackageODCSHelper = new List<HELPER.GetHistoricalPackage.PackageODCSHelper>();
                    foreach (Claro.SIACU.Web.WebApplication.PostpaidService.PackageODCSPrePaid item in objTotalMbPurchasedPackageResponsePospaid.lstPackageODCS)
                    {
                        lstPackageODCSHelper.Add(new HELPER.GetHistoricalPackage.PackageODCSHelper()
                        {
                            NameBag = item.NameBag,
                            validity = item.validity,
                            cost = item.cost,
                            TypePackage = item.TypePackage,
                            AcquisitionDate = item.AcquisitionDate,
                            State = item.State.ToUpper() == "PENDIENTE" ? "ACTIVO" : item.State,
                            //TOBE
                            TypeValidity = item.TypeValidity,
                            TypePurchase = item.TypePurchase,
                            TypeBalance = item.TypeBalance
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, objTotalMbPurchasedPackageRequestPospaid.audit.transaction, ex.Message);
            }
            objHistoricalPackageHelper.IsVisibleMensaje = objTotalMbPurchasedPackageResponsePospaid.IsVisibleMensaje;
            objHistoricalPackageHelper.lstPackageODCS = lstPackageODCSHelper;
            objHistoricalPackageHelper.strMensaje = objTotalMbPurchasedPackageResponsePospaid.strMensaje;
            objHistoricalPackageHelper.strMensajeAlert = objTotalMbPurchasedPackageResponsePospaid.strMensajeAlert;
            objHistoricalPackageHelper.TotalMBCicle = objTotalMbPurchasedPackageResponsePospaid.TotalMBCicle;
            objHistoricalPackageHelper.TotalRows = objTotalMbPurchasedPackageResponsePospaid.TotalRows;
            objHistoricalPackageHelper.FlagOne = objTotalMbPurchasedPackageResponsePospaid.FlagOne;

            Areas.Dashboard.Models.PostpaidDataService.HistoricalPackageModel objHistoricalPackageModel = new Areas.Dashboard.Models.PostpaidDataService.HistoricalPackageModel()
            {
                objHistoricalPackageHelper = objHistoricalPackageHelper
            };


            return Json(new { data = objHistoricalPackageModel });
        }

        public JsonResult GetJsonHistoricalPackageOneJanus(string strIdSession, string strFlagOne, PostpaidService.TotalMbPurchasedPackageRequestPospaid objTotalMbPurchasedPackageRequestPospaid, string plataforma)
        {

            PostpaidService.TotalMbPurchasedPackageResponsePospaid objTotalMbPurchasedPackageResponsePospaid = new PostpaidService.TotalMbPurchasedPackageResponsePospaid();

            HELPER.GetHistoricalPackage.HistoricalPackageHelper objHistoricalPackageHelper = new HELPER.GetHistoricalPackage.HistoricalPackageHelper();
            List<HELPER.GetHistoricalPackage.PackageODCSHelper> lstPackageODCSHelper = null;
            objTotalMbPurchasedPackageRequestPospaid.objAudit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);


            try
            {
                if (strFlagOne == "1")
                {
                    objTotalMbPurchasedPackageResponsePospaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.TotalMbPurchasedPackageResponsePospaid>(
                    () => { return oServicepost.GetDataOnePackage(objTotalMbPurchasedPackageRequestPospaid, plataforma); });
                }
                else
                {
                    objTotalMbPurchasedPackageResponsePospaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.TotalMbPurchasedPackageResponsePospaid>(
                    () => { return oServicepost.GetDataJanusPackage(objTotalMbPurchasedPackageRequestPospaid); });
                }


                if (objTotalMbPurchasedPackageResponsePospaid.lstPackageODCS != null && objTotalMbPurchasedPackageResponsePospaid.lstPackageODCS.Count > 0)
                {
                    lstPackageODCSHelper = new List<HELPER.GetHistoricalPackage.PackageODCSHelper>();
                    foreach (Claro.SIACU.Web.WebApplication.PostpaidService.PackageODCSPrePaid item in objTotalMbPurchasedPackageResponsePospaid.lstPackageODCS)
                    {
                        lstPackageODCSHelper.Add(new HELPER.GetHistoricalPackage.PackageODCSHelper()
                        {
                            NameBag = item.NameBag,
                            validity = item.validity,
                            cost = item.cost,
                            TypePackage = item.TypePackage,
                            AcquisitionDate = item.AcquisitionDate,
                            State = item.State,
                            //TOBE
                            TypeValidity = item.TypeValidity,
                            TypePurchase = item.TypePurchase

                        });
                    }
                }

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, objTotalMbPurchasedPackageRequestPospaid.audit.transaction, ex.Message);
            }
            objHistoricalPackageHelper.IsVisibleMensaje = objTotalMbPurchasedPackageResponsePospaid.IsVisibleMensaje;
            objHistoricalPackageHelper.lstPackageODCS = lstPackageODCSHelper;
            objHistoricalPackageHelper.strMensaje = objTotalMbPurchasedPackageResponsePospaid.strMensaje;
            objHistoricalPackageHelper.strMensajeAlert = objTotalMbPurchasedPackageResponsePospaid.strMensajeAlert;
            objHistoricalPackageHelper.TotalMBCicle = objTotalMbPurchasedPackageResponsePospaid.TotalMBCicle;
            objHistoricalPackageHelper.TotalRows = objTotalMbPurchasedPackageResponsePospaid.TotalRows;
            objHistoricalPackageHelper.FlagOne = objTotalMbPurchasedPackageResponsePospaid.FlagOne;

            Areas.Dashboard.Models.PostpaidDataService.HistoricalPackageModel objHistoricalPackageModel = new Areas.Dashboard.Models.PostpaidDataService.HistoricalPackageModel()
            {
                objHistoricalPackageHelper = objHistoricalPackageHelper
            };


            return Json(new { data = objHistoricalPackageModel });
        }

        public JsonResult PostReportBalanceShared(string strIdSession, PostpaidService.SharedBagRequestPostPaid objSharedBagRequestPostPaid)
        {
            PostpaidService.SharedBagResponsePostPaid objGetSharedBagResponse = new PostpaidService.SharedBagResponsePostPaid();

            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBag> lstSharedBag = new List<HELPER.AccountSharedBag.AccountSharedBag>();

            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBagLine> lstSharedBagLine = new List<HELPER.AccountSharedBag.AccountSharedBagLine>();


            objSharedBagRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            Areas.Dashboard.Models.PostpaidDataAccount.AccountSharedBagModel oPostDataSharedBagModel = new Areas.Dashboard.Models.PostpaidDataAccount.AccountSharedBagModel()
            {
                Account = objSharedBagRequestPostPaid.Account,
                Line = objSharedBagRequestPostPaid.Telephone,
                lstSharedBag = null,
                lstSharedBagLine = null
            };
            try
            {
                objGetSharedBagResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.SharedBagResponsePostPaid>(
                    () => { return oServicepost.GetDataBalanceShared(objSharedBagRequestPostPaid); });


                if (objGetSharedBagResponse.ListSharedBag != null && objGetSharedBagResponse.ListSharedBag.Count > 0)
                {

                    foreach (PostpaidService.SharedBagPostPaid item in objGetSharedBagResponse.ListSharedBag)
                    {
                        lstSharedBag.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBag()
                        {
                            Account = item.CUENTA,
                            Bag = item.BOLSA,
                            Line = item.LINEA,
                            Unit = item.UNIDAD,
                            Stopper = item.TOPE,
                            Consumption = item.CONSUMO,
                            Balance = item.SALDO,
                            DateValidity = item.FECHAVIGENCIA,
                            Optional1 = item.OPCIONAL1,
                            Optional2 = item.OPCIONAL2,
                            GroupBag = item.GRUPO_BOLSA
                        });
                    }
                }
                else
                {
                    lstSharedBag.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBag()
                    {
                        Account = "",
                        Bag = "",
                        Line = "",
                        Unit = "",
                        Stopper = "",
                        Consumption = "",
                        Balance = "",
                        DateValidity = "",
                        Optional1 = "",
                        Optional2 = "",
                        GroupBag = ""
                    });
                }
                if (objGetSharedBagResponse.ListSharedBagLine != null && objGetSharedBagResponse.ListSharedBagLine.Count > 0)
                {

                    foreach (PostpaidService.SharedBagPostPaid item in objGetSharedBagResponse.ListSharedBagLine)
                    {
                        lstSharedBagLine.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBagLine()
                        {
                            Account = item.CUENTA,
                            Bag = item.BOLSA,
                            Line = item.LINEA,
                            Unit = item.UNIDAD,
                            Stopper = item.TOPE,
                            Consumption = item.CONSUMO,
                            Balance = item.SALDO,
                            DateValidity = item.FECHAVIGENCIA,


                        });
                    }
                }
                else
                {
                    lstSharedBagLine.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBagLine()
                    {

                        Account = "",
                        Bag = "",
                        Line = "",
                        Unit = "",
                        Stopper = "",
                        Consumption = "",
                        Balance = "",
                        DateValidity = ""
                    });
                }


            }
            catch (Exception ex)
            {


                oPostDataSharedBagModel.Error = KEY.AppSettings("strMsgErrorTransaccion");
                Claro.Web.Logging.Error(strIdSession, objSharedBagRequestPostPaid.audit.transaction, ex.Message);
            }





            oPostDataSharedBagModel.lstSharedBag = lstSharedBag;
            oPostDataSharedBagModel.lstSharedBagLine = lstSharedBagLine;
            oPostDataSharedBagModel.Error = objGetSharedBagResponse.Message;
            oPostDataSharedBagModel.MessageTypeCustomer = objGetSharedBagResponse.MessageTypeCustomer;
            oPostDataSharedBagModel.CountSharedBag = objGetSharedBagResponse.CountSharedBag;

            string path = oExcelHelper.ExportExcel(oPostDataSharedBagModel, TemplateExcel.CONST_BALANCESHARED);


            return Json(path);
        }

        public JsonResult GetJsonOrderConsumption(string strIdSession, PostpaidService.TypeOrderRequestCommon objTypeOrderRequestCommon)
        {
            List<HELPER.ConsumptionType.ConsumptionTypeHelper> lstTypeOrderResponseHelper = new List<HELPER.ConsumptionType.ConsumptionTypeHelper>();
            PostpaidService.TypeOrderResponseCommon objTypeOrderResponseCommon = new PostpaidService.TypeOrderResponseCommon();
            objTypeOrderRequestCommon.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            try
            {
                objTypeOrderResponseCommon = Claro.Web.Logging.ExecuteMethod<PostpaidService.TypeOrderResponseCommon>(() => { return oServicepost.GetTypeOrder(objTypeOrderRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objTypeOrderRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(objTypeOrderRequestCommon.audit.transaction);
            }

            if (objTypeOrderResponseCommon.ListItem != null)
            {


                foreach (PostpaidService.ListItem item in objTypeOrderResponseCommon.ListItem)
                {
                    lstTypeOrderResponseHelper.Add(new HELPER.ConsumptionType.ConsumptionTypeHelper()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }


            }

            return Json(new { data = lstTypeOrderResponseHelper });

        }


    }
}