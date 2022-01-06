using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Web.WebApplication.ColivingService;
using Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.CustomerSearch;
using Claro.SIACU.Web.WebApplication.Areas.Coliving.Controllers;

namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Controllers
{
    public class SearchCustomerController : Controller
    {
        ColivingServiceClient oColivingService = new ColivingServiceClient();
        CommonController cCommon = new CommonController();

        public DocumentCustomerModel model2 = null;

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SIACU_Type_Search_Customer()
        {
            return PartialView();
        }
        #region SearchCustomerLine
        /// <summary>
        /// ActionResult para la vista SIACU_Search_Customer_Line, ademas se esta pasando algunos valores por llave .
        /// </summary>
        /// <returns></returns> 
        public ActionResult SIACU_Search_Customer_Line()
        {
            var strKeyWs = KEY.AppSettings("strKeyWebSpecial");
            var strKeyPv = KEY.AppSettings("strKeyPostVenta");

            ViewBag.nameLinkWs = strKeyWs.Split('|')[0];
            ViewBag.urlWs = strKeyWs.Split('|')[1];

            ViewBag.nameLinkPv = strKeyPv.Split('|')[0];
            ViewBag.urlPv = strKeyPv.Split('|')[1];

            ViewBag.migradoOne = KEY.AppSettings("strKeyMigradoOne").ToString();
            ViewBag.strPrepagoOlo = KEY.AppSettings("strPrepagoOlo").ToString();
            return PartialView();
        }
        /// <summary>
        /// JsonResult SIACU_Search_Customer_Line.cshtml que obtiene datos del servicio SearchCustomerManagment,RetrieveSubscription | objObtenerDatosClienteResponse.
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="objSearchType">objSearchType</param>
        /// <returns></returns> 
        public JsonResult DataCustomerLine(string strIdSession, SearchTypeModel objSearchType = null)
        {
            var codBusquedaXLinea = KEY.AppSettings("strKeyTipoBusqueda").Split('|')[0].Split(';')[0];
            ConsultaLineaResponse consultaLineaCuenta = ConsultaLineaCuenta(strIdSession, codBusquedaXLinea, objSearchType.LineNumber);

            LineCustomerModel customerSearchModel = null;

            if (consultaLineaCuenta != null)
            {
                var noMigradoFullStack = KEY.AppSettings("strKeyMigradoFullStack").Split('|')[0].Split(';')[1];
                var migradoFullStack = KEY.AppSettings("strKeyMigradoFullStack").Split('|')[1].Split(';')[1];
                if (consultaLineaCuenta.ResponseDescription == migradoFullStack)
                {
                    var objSearchCustomerResponse = SearchCustomerService(strIdSession, objSearchType);
                    var objAgreementManagement = RetrieveSubscriptionService(strIdSession, objSearchType);

                    if (objSearchCustomerResponse != null && objAgreementManagement != null)
                    {
                        customerSearchModel = new LineCustomerModel()
                        {
                            DocumentType = objSearchCustomerResponse.DocumentType,
                            DocumentTypeDescription = objSearchCustomerResponse.DescriptionDocumentType,
                            DocumentNumber = objSearchCustomerResponse.DocumentNumber,
                            CustomerName = objSearchCustomerResponse.CustomerName,
                            MigrateOne = consultaLineaCuenta.ResponseDescription,
                            Address = objSearchCustomerResponse.Address,
                            AccountNumber = objAgreementManagement.Subscriptions[0].CustomerId,
                            ServiceIdentifier = objAgreementManagement.Subscriptions[0].ServiceIdentifier,
                            ProductType = objAgreementManagement.Subscriptions[0].ProductType,
                            PoName = objAgreementManagement.Subscriptions[0].PoName,
                            SubscriptionStatus = objAgreementManagement.Subscriptions[0] != null ? objAgreementManagement.Subscriptions[0].SubscriptionStatus : null
                        };
                        CustomerInfoResponse objCustomerInfoResponse = cCommon.GetCustomerInfoService(strIdSession, new SearchTypeModel()
                        {
                            DocumentType = objSearchCustomerResponse.DocumentType,
                            DocumentNumber = objSearchCustomerResponse.DocumentNumber
                        });
                        if (objCustomerInfoResponse != null)
                        {
                            customerSearchModel.Segment = objCustomerInfoResponse.CustomerType;
                        }
                        else
                        {
                            customerSearchModel = null;
                            return Json(new { data = customerSearchModel });
                        }
                    }
                }
                else if (consultaLineaCuenta.ResponseDescription == noMigradoFullStack)
                {
                    ObtenerDatosClienteResponseColiving objObtenerDatosClienteResponse = ObtenerDatosClienteService(strIdSession, objSearchType);
                    if (objObtenerDatosClienteResponse != null)
                    {
                        customerSearchModel = new LineCustomerModel()
                        {
                            CustomerName = objObtenerDatosClienteResponse.CustomerName,
                            DocumentTypeDescription = objObtenerDatosClienteResponse.DescriptionDocumentType,
                            DocumentType = objObtenerDatosClienteResponse.DocumentType,
                            DocumentNumber = objObtenerDatosClienteResponse.DocumentNumber,
                            MigrateOne = consultaLineaCuenta.ResponseDescription,
                            Address = objObtenerDatosClienteResponse.CustomerAddress,
                            ProductType = objObtenerDatosClienteResponse.ProductType
                        };
                        if (objObtenerDatosClienteResponse.SubscriptionsPostPaid != null && objObtenerDatosClienteResponse.SubscriptionsPostPaid.Count > 0)
                        {
                            customerSearchModel.AccountNumber = objObtenerDatosClienteResponse.SubscriptionsPostPaid.FirstOrDefault().AccountNumber;
                            customerSearchModel.PoName = objObtenerDatosClienteResponse.SubscriptionsPostPaid.FirstOrDefault().RatePlan;
                            customerSearchModel.Segment = objObtenerDatosClienteResponse.SubscriptionsPostPaid.FirstOrDefault().Segment;
                            customerSearchModel.SubscriptionStatus = objObtenerDatosClienteResponse.SubscriptionsPostPaid.FirstOrDefault().LineStatus;
                            customerSearchModel.ServiceIdentifier = objObtenerDatosClienteResponse.SubscriptionsPostPaid.FirstOrDefault().LineNumber;
                            customerSearchModel.ProductType = objObtenerDatosClienteResponse.SubscriptionsPostPaid.FirstOrDefault().ProductType;
                        }
                        else if (objObtenerDatosClienteResponse.SubscriptionsPrepaid != null && objObtenerDatosClienteResponse.SubscriptionsPrepaid.Count > 0)
                        {
                            customerSearchModel.SubscriptionStatus = objObtenerDatosClienteResponse.SubscriptionsPrepaid.FirstOrDefault().LineStatus;
                            customerSearchModel.ServiceIdentifier = objObtenerDatosClienteResponse.SubscriptionsPrepaid.FirstOrDefault().LineNumber;
                            customerSearchModel.Segment = objObtenerDatosClienteResponse.SubscriptionsPrepaid.FirstOrDefault().Segment;                                          
                        }
                    }
                }
            }
            return Json(new { data = customerSearchModel });
        }
        #endregion
        #region SearchCustomerAccount
        /// <summary>
        /// ActionResult para la vista SIACU_Search_Customer_Account, ademas se esta pasando algunos valores por llave .
        /// </summary>
        /// <returns></returns> 
        public ActionResult SIACU_Search_Customer_Account(string CustomerId)
        {
            var strKeyWs = KEY.AppSettings("strKeyWebSpecial");
            var strKeyPv = KEY.AppSettings("strKeyPostVenta");

            ViewBag.nameLinkWs = strKeyWs.Split('|')[0];
            ViewBag.urlWs = strKeyWs.Split('|')[1];

            ViewBag.nameLinkPv = strKeyPv.Split('|')[0];
            ViewBag.urlPv = strKeyPv.Split('|')[1];

            ViewBag.migradoOne = KEY.AppSettings("strKeyMigradoOne").ToString();
            ViewBag.strPrepagoOlo = KEY.AppSettings("strPrepagoOlo").ToString();

            ViewBag.CustomerId = CustomerId;
            return View();
        }
        /// <summary>
        /// JsonResult SIACU_Search_Customer_Account.cshtml que obtiene datos del servicio SearchCustomerManagment,RetrieveSubscription | objObtenerDatosClienteResponse.
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="objSearchType">objSearchType</param>
        /// <returns></returns> 
        public JsonResult DataCustomerAccount(String strIdSession, SearchTypeModel objSearchType = null)
        {
            var codBusquedaXCuenta = KEY.AppSettings("strKeyTipoBusqueda").Split('|')[1].Split(';')[0];
            var consultaLineaCuenta = new ConsultaLineaResponse();
            consultaLineaCuenta = ConsultaLineaCuenta(strIdSession, codBusquedaXCuenta, objSearchType.CustomerId);
            AccountCustomerModel model = null;
            var noMigradoFullStack = KEY.AppSettings("strKeyMigradoFullStack").Split('|')[0].Split(';')[1];
            var migradoFullStack = KEY.AppSettings("strKeyMigradoFullStack").Split('|')[1].Split(';')[1];
            if (consultaLineaCuenta != null)
            {
                if (consultaLineaCuenta.ResponseDescription == migradoFullStack)
                {
                    SearchCustomerResponseColiving objSearchCustomerResponse = SearchCustomerService(strIdSession, objSearchType);
                    RetrieveSubscriptionResponse objRetrieveSubscriptionResponse = RetrieveSubscriptionService(strIdSession, objSearchType);
                    CustomerInfoResponse objCustomerInfoResponse = cCommon.GetCustomerInfoService(strIdSession, objSearchType);

                    if (objSearchCustomerResponse != null)
                    {
                        AccountSubscription item = null;
                        var lista = new List<AccountSubscription>();
                        model = new AccountCustomerModel();
                        if (objRetrieveSubscriptionResponse.Subscriptions != null && objRetrieveSubscriptionResponse.Subscriptions.Count > 0)
                        {
                            foreach (var x in objRetrieveSubscriptionResponse.Subscriptions)
                            {
                                item = new AccountSubscription();
                                item.LineNumber = x.ServiceIdentifier;
                                item.LineStatus = x.SubscriptionStatus;
                                item.RatePlan = x.PoName;
                                item.ProductType = x.ProductType;
                                lista.Add(item);
                            }
                            model.ProductType = objRetrieveSubscriptionResponse.Subscriptions[0].ProductType;
                            model.AccountNumber = objRetrieveSubscriptionResponse.Subscriptions[0].CustomerId;
                        }
                        if (objCustomerInfoResponse != null)
                        {
                            model.Segment = objCustomerInfoResponse.CustomerType;
                        }
                        else
                        {
                            model = null;
                            return Json(new { data = model });
                        }
                        model.DocumentType = objSearchCustomerResponse.DocumentType;
                        model.DocumentTypeDescription = objSearchCustomerResponse.DescriptionDocumentType;
                        model.DocumentNumber = objSearchCustomerResponse.DocumentNumber;
                        model.CustomerName = objSearchCustomerResponse.CustomerName;
                        model.Address = objSearchCustomerResponse.Address;
                        model.MigrateOne = consultaLineaCuenta.ResponseDescription;
                        model.ListSuscription = lista;
                    }
                }
                else if (consultaLineaCuenta.ResponseDescription == noMigradoFullStack)
                {
                    ObtenerDatosClienteResponseColiving objObtenerDatosClienteResponse = ObtenerDatosClienteService(strIdSession, objSearchType);
                    if (objObtenerDatosClienteResponse != null)
                    {
                        AccountSubscription item = null;
                        var lista = new List<AccountSubscription>();
                        model = new AccountCustomerModel();
                        if (objObtenerDatosClienteResponse.SubscriptionsPostPaid != null && objObtenerDatosClienteResponse.SubscriptionsPostPaid.Count > 0)
                        {
                            foreach (var x in objObtenerDatosClienteResponse.SubscriptionsPostPaid)
                            {
                                item = new AccountSubscription();
                                item.LineNumber = x.LineNumber;
                                item.LineStatus = x.LineStatus;
                                item.RatePlan = x.RatePlan;
                                item.ProductType = x.ProductType;
                                lista.Add(item);
                            }
                            model.AccountNumber = objObtenerDatosClienteResponse.SubscriptionsPostPaid.FirstOrDefault().AccountNumber;
                            model.ProductType = objObtenerDatosClienteResponse.SubscriptionsPostPaid.FirstOrDefault().ProductType;
                            model.Segment = objObtenerDatosClienteResponse.SubscriptionsPostPaid.FirstOrDefault().Segment;
                        }
                        model.DocumentType = objObtenerDatosClienteResponse.DocumentType;
                        model.DocumentTypeDescription = objObtenerDatosClienteResponse.DescriptionDocumentType;
                        model.DocumentType = objObtenerDatosClienteResponse.DocumentType;
                        model.DocumentNumber = objObtenerDatosClienteResponse.DocumentNumber;
                        model.CustomerName = objObtenerDatosClienteResponse.CustomerName;
                        model.Address = objObtenerDatosClienteResponse.CustomerAddress;
                        model.MigrateOne = consultaLineaCuenta.ResponseDescription;
                        model.ListSuscription = lista;
                    }
                }
            }
            JsonResult jsonresult = Json(new { data = model });
            jsonresult.MaxJsonLength = 500000000;
            return jsonresult;
        }
        #endregion

        #region SearchCustomerDocument

        /// <summary>
        /// ActionResult para la vista SIACU_Search_Customer_Doc_Type(carga el tipo de búsqueda)
        /// </summary>
        /// <returns></returns> 
        public ActionResult SIACU_Search_Customer_Doc_Type()
        {
            var strKeyWs = KEY.AppSettings("strKeyWebSpecial");
            ViewBag.nameLinkWs = strKeyWs.Split('|')[0];
            ViewBag.urlWs = strKeyWs.Split('|')[1];

            var strKeyPv = KEY.AppSettings("strKeyPostVenta");
            ViewBag.nameLinkPv = strKeyPv.Split('|')[0];
            ViewBag.urlPv = strKeyPv.Split('|')[1];

            ViewBag.migradoOne = KEY.AppSettings("strKeyMigradoOne").ToString();
            ViewBag.strPrepagoOlo = KEY.AppSettings("strPrepagoOlo").ToString();

            var strKeyMigradoFullStack = KEY.AppSettings("strKeyMigradoFullStack");
            ViewBag.strKeyMigradoFullStack = strKeyMigradoFullStack;

            var estatus = KEY.AppSettings("strKeySubscriptionStatus").ToString();
            ViewBag.estatus = estatus;

            ViewBag.UrlDestino = String.Format("{0}|{1}", KEY.AppSettings("strKeyUrlDestinoSISTEC"), KEY.AppSettings("strKeyUrlDestinoTCRM")); 

            return PartialView();
        }
        /// <summary>
        /// ActionResult para la vista SIACU_Search_Customer_Doc_Type.cshtml, ademas se esta pasando algunos valores por llave.
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="objSearchType">objSearchType</param>
        /// <returns></returns> 
        public JsonResult DataCustomerDocument(string strIdSession, SearchTypeModel objSearchType = null)
        {
            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);

            DocumentCustomerModel model = new DocumentCustomerModel();
            var listPrePaidService = new List<PrePaidServiceModel>();
            var listPostPaidService = new List<PostPaidServiceModel>();

            List<string> subscriptionStatus = null;
            List<string> typeSearch = null;
            List<string> strkeyOrigenInfo = null;
            List<string> typeDocument = null;

            try
            {
                subscriptionStatus = KEY.AppSettings("strKeySubscriptionStatus").Split('|').ToList();
                typeSearch = KEY.AppSettings("strKeyTipoBusqueda").Split('|').ToList();
                strkeyOrigenInfo = KEY.AppSettings("strkeyOrigenInfo").Split('|').ToList();
                typeDocument = KEY.AppSettings("strkeyDocument").Split('|').ToList();

                var typeDocumentMigrate = "";
                if (typeDocument != null)
                {
                    foreach (var item in typeDocument)
                    {
                        if (item.Split(';')[3] == objSearchType.DocumentType)
                        {
                            typeDocumentMigrate = item.Split(';')[1];
                            break;
                        }
                    }
                }

                var documentSearchNoMigrate = "";
                if (typeDocument != null)
                {
                    foreach (var item in typeDocument)
                    {
                        if (item.Split(';')[3] == objSearchType.DocumentType)
                        {
                            documentSearchNoMigrate = item.Split(';')[2];
                            break;
                        }
                    }
                }
                objSearchType.DocumentType = typeDocumentMigrate;
                var objSearchCustomerResponse = SearchCustomerService(strIdSession, objSearchType);

                objSearchType.DocumentType = documentSearchNoMigrate;
                var objObtenerDatosResponse = ObtenerDatosClienteService(strIdSession, objSearchType);

                if (objSearchCustomerResponse != null)
                {
                    model.DocumentTypeDescription = objSearchCustomerResponse.DescriptionDocumentType;
                    model.DocumentType = objSearchCustomerResponse.DocumentType;
                    model.DocumentNumber = objSearchCustomerResponse.DocumentNumber;
                    model.CustomerName = objSearchCustomerResponse.CustomerName;
                    if (!String.IsNullOrEmpty(objSearchCustomerResponse.Address))
                    {
                    model.Address = objSearchCustomerResponse.Address;
                }
                    else
                    {
                        if (objObtenerDatosResponse != null)
                        {
                            model.Address = objObtenerDatosResponse.CustomerAddress;
                        }
                    }
                }
                else if (objObtenerDatosResponse != null)
                {
                    model.DocumentTypeDescription = objObtenerDatosResponse.DescriptionDocumentType;
                    model.DocumentType = objObtenerDatosResponse.DocumentType;
                    model.DocumentNumber = objObtenerDatosResponse.DocumentNumber;
                    model.CustomerName = objObtenerDatosResponse.CustomerName;
                    model.Address = objObtenerDatosResponse.CustomerAddress;
                }
                else
                {
                    model = null;
                    return Json(new { data = model });
                }

                #region Detalle

                #region RetrieveSubscriptionService
                objSearchType.DocumentType = typeDocumentMigrate;
                RetrieveSubscriptionResponse retrieveSubscriptionResponse = RetrieveSubscriptionService(strIdSession, objSearchType);

                if (retrieveSubscriptionResponse != null)
                {
                    if (retrieveSubscriptionResponse.Subscriptions != null && retrieveSubscriptionResponse.Subscriptions.Count > 0)
                    {
                        var groupedSubscriptionsRetrieveList = retrieveSubscriptionResponse.Subscriptions
                                                                   .GroupBy(u => u.CustomerId)
                                                                   .Select(grp => grp.ToList())
                                                                   .ToList();

                        foreach (var itemSubscriptionRetrieve in groupedSubscriptionsRetrieveList)
                        {
                            var proPaiService = new PostPaidServiceModel()
                            {
                                BillingAccountId = itemSubscriptionRetrieve.FirstOrDefault().CustomerId,
                                ProductType = itemSubscriptionRetrieve.FirstOrDefault().ProductType
                            };
                            CustomerInfoResponse objCustomerInfoResponse = cCommon.GetCustomerInfoService(strIdSession, new SearchTypeModel()
                            {
                                CustomerId = itemSubscriptionRetrieve.FirstOrDefault().CustomerId
                            });

                            if (objCustomerInfoResponse != null)
                            {
                                proPaiService.CustomerType = objCustomerInfoResponse.CustomerType;
                            }
                            else
                            {
                                model = null;
                                return Json(new { data = model });
                            }

                            proPaiService.MigrateOne = "SI";

                            var listSubcriptionRetriveActive = new List<AccountSubscription>();
                            var listSubcriptionRetrieveInactive = new List<AccountSubscription>();

                            foreach (var item in itemSubscriptionRetrieve)
                            {
                                if (item.SubscriptionStatus.ToUpper() == subscriptionStatus[1].Split(';')[1].ToString().ToUpper())
                                {
                                    var subscriptionRetrive = new AccountSubscription()
                                    {
                                        LineNumber = item.ServiceIdentifier,
                                        RatePlan = item.PoName,
                                        LineStatus = item.SubscriptionStatus
                                    };
                                    listSubcriptionRetriveActive.Add(subscriptionRetrive);
                                }
                                else
                                {
                                    var subscriptionRetrive = new AccountSubscription()
                                    {
                                        LineNumber = item.ServiceIdentifier,
                                        RatePlan = item.PoName,
                                        LineStatus = item.SubscriptionStatus
                                    };
                                    listSubcriptionRetrieveInactive.Add(subscriptionRetrive);
                                }
                            }

                            proPaiService.SubscriptionActive = listSubcriptionRetriveActive;
                            proPaiService.SubscriptionInactive = listSubcriptionRetrieveInactive;

                            proPaiService.CountActivateLine = listSubcriptionRetriveActive.Count().ToString();
                            proPaiService.CountInactiveLine = listSubcriptionRetrieveInactive.Count().ToString();

                            listPostPaidService.Add(proPaiService);
                        }
                    }
                }

                #endregion

                #region ObtenerDatosClienteResponse

                if (objObtenerDatosResponse != null)
                {
                    #region PostPago
                    if (objObtenerDatosResponse.SubscriptionsPostPaid != null && objObtenerDatosResponse.SubscriptionsPostPaid.Count > 0)
                    {
                        var groupedSubscriptionsPostPaidList = objObtenerDatosResponse.SubscriptionsPostPaid
                                                               .GroupBy(u => u.AccountNumber)
                                                               .Select(grp => grp.ToList())
                                                               .ToList();

                        foreach (var itemGroupedSubscription in groupedSubscriptionsPostPaidList)
                        {

                            var proPaiService = new PostPaidServiceModel();

                            if (itemGroupedSubscription[0].OrigenInfo == strkeyOrigenInfo[1].Split(';')[0])
                            {
                                proPaiService.MigrateOne = strkeyOrigenInfo[1].Split(';')[1];
                            }
                            else
                            {
                                proPaiService.MigrateOne = "NO";
                            }
                            proPaiService.BillingAccountId = itemGroupedSubscription[0].AccountNumber;
                            proPaiService.ProductType = itemGroupedSubscription[0].ProductType;
                            proPaiService.CustomerType = itemGroupedSubscription[0].Segment;
                            proPaiService.OrigenInfoPost = itemGroupedSubscription[0].OrigenInfo;

                            var listSubcriptionActive = new List<AccountSubscription>();
                            var listSubcriptionInactive = new List<AccountSubscription>();

                            //Agrupar las lineas activas y las lineas inactivas por cada cuenta
                            foreach (var item in itemGroupedSubscription)
                            {
                                if (item.LineStatus.ToUpper() == subscriptionStatus[1].Split(';')[1].ToString().ToUpper())
                                {
                                    var subscription = new AccountSubscription()
                                    {
                                        LineNumber = item.LineNumber,
                                        RatePlan = item.RatePlan,
                                        LineStatus = item.LineStatus
                                    };
                                    listSubcriptionActive.Add(subscription);
                                }
                                else
                                {
                                    var subscription = new AccountSubscription()
                                    {
                                        LineNumber = item.LineNumber,
                                        RatePlan = item.RatePlan,
                                        LineStatus = item.LineStatus
                                    };
                                    listSubcriptionInactive.Add(subscription);
                                }
                            }
                            proPaiService.SubscriptionActive = listSubcriptionActive;
                            proPaiService.SubscriptionInactive = listSubcriptionInactive;

                            proPaiService.CountActivateLine = listSubcriptionActive.Count().ToString();
                            proPaiService.CountInactiveLine = listSubcriptionInactive.Count().ToString();

                            listPostPaidService.Add(proPaiService);
                        }
                    }
                    #endregion

                    #region Prepago

                    if (objObtenerDatosResponse.SubscriptionsPrepaid != null && objObtenerDatosResponse.SubscriptionsPrepaid.Count > 0)
                    {

                        foreach (var item in objObtenerDatosResponse.SubscriptionsPrepaid)
                        {
                            if (!item.ProductType.ToUpper().EndsWith("OLO"))
                            {
                                if (item.origenInfoPre.ToUpper().Trim() != strkeyOrigenInfo[1].Split(';')[0])
                                {
                                    //Verificar si la linea Prepago ha migrado -->Type="1"
                                    ConsultaLineaResponse consultaLineaCuenta = ConsultaLineaCuenta(strIdSession, typeSearch[1].Split(';')[0], item.LineNumber);

                                    if (consultaLineaCuenta != null)
                                    {
                                        item.MigrateOne = consultaLineaCuenta.ResponseDescription;
                                    }
                                    else
                                    {
                                        model = null;
                                        return Json(new { data = model });
                                    }
                                    var subscriptionPrepago = new PrePaidServiceModel()
                                    {
                                        LineNumber = item.LineNumber,
                                        LineStatus = item.LineStatus,
                                        MigrateOne = item.MigrateOne
                                    };
                                    listPrePaidService.Add(subscriptionPrepago);
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                #endregion

                model.ListPostPaidService = listPostPaidService;
                model.ListPrePaidService = listPrePaidService;

                Config.model = model;
                model2 = model;
                
                
                string strDocumentNumberVista = objSearchType.DocumentNumber;
                string strConsultDateVista = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string strTextVista = KEY.AppSettings("strConstMsgBusquedaDocumento") + Claro.SIACU.Constants.DoubleScript + Claro.SIACU.Constants.DateHours + strConsultDateVista + Claro.SIACU.Constants.DoubleScript + "Documento: " + strDocumentNumberVista;
                Claro.Web.Logging.ExecuteMethod<string>(() =>
                {
                    return App_Code.Common.InsertAudit(objaudit, strDocumentNumberVista, KEY.AppSettings("strAudiTraPantallaBusquedaDocumento"), strTextVista);
                });
                JsonResult jsonresult = Json(new { data = model});
                jsonresult.MaxJsonLength = 500000000;
                return jsonresult;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objaudit.Session, objaudit.transaction, ex.Message);
                throw new Claro.MessageException("");
            }
        }
        public ActionResult ListService()
        {
            return PartialView();
        }

        public ActionResult SIACU_ListTechnicalServiceOST(String strLineNumber, String strClientType, String strClient, String strMigrateStatus)
        {
            ViewBag.LineNumber = strLineNumber;
            ViewBag.ClientType = strClientType;
            ViewBag.Client = strClient;
            ViewBag.MigrateStatus = strMigrateStatus;
            ViewBag.UrlDestino = String.Format("{0}|{1}", KEY.AppSettings("strKeyUrlDestinoSISTEC") , KEY.AppSettings("strKeyUrlDestinoTCRM")); 

            return PartialView();
        }


        /// <summary>
        /// Método que obtiene el listado de la OST de LEGADO y ONE
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="SearchOstModel">SearchOstModel</param>
        /// <returns>JsonResult</returns> 
        public JsonResult GetListOST(String strIdSession, SearchOstModel objSearchOst)
        {
            ListOSTResponseColiving objListOSTResponseColiving = null;
            objListOSTResponseColiving = GetListOST_Legacy(strIdSession, objSearchOst);
            List<OstModel> objOstModel = new List<OstModel>();
            if (objListOSTResponseColiving.TechnicalServicesOSTType != null)
            {
                foreach (TechnicalServicesOSTType item in objListOSTResponseColiving.TechnicalServicesOSTType)
                {
                    objOstModel.Add(new OstModel()
                    {
                        NroOst = item.idOst,
                        Cac = item.Cac,
                        FechaGeneracion = item.FechaGeneracion,
                        Marca = item.Marca,
                        Modelo = item.Modelo,
                        Imei = item.Imei,
                        Origen = Constants.ConstLegacy
                    });
                }
            }
            objListOSTResponseColiving = GetListOSTDetails_One(strIdSession, objSearchOst);
            if (objListOSTResponseColiving.TechnicalServicesOSTType != null)
            {
                foreach (TechnicalServicesOSTType item in objListOSTResponseColiving.TechnicalServicesOSTType)
                {
                    objOstModel.Add(new OstModel()
                    {
                        NroOst = item.idOst,
                        Cac = item.Cac,
                        FechaGeneracion = item.FechaGeneracion,
                        Marca = item.Marca,
                        Modelo = item.Modelo,
                        Imei = item.Imei,
                        Origen = Constants.ConstOne
                    });
                }
            }
            

            return Json(new {data= objOstModel });
        }


        /// <summary>
        /// Método que obtiene el listado de la OST de LEGADO
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="SearchOstModel">SearchOstModel</param>
        /// <returns>JsonResult</returns> 
        public ListOSTResponseColiving GetListOST_Legacy(String strIdSession, SearchOstModel objSearchOst)
        {
            ListOSTRequestColiving objListOSTRequestColiving = null;
            ListOSTResponseColiving objListOSTResponseColiving = null;
            objListOSTRequestColiving = new ListOSTRequestColiving()
            {
                IdBusca = objSearchOst.IdBusca,
                IdCAC = objSearchOst.IdCAC,
                IdCriterio = objSearchOst.IdCriterio,
                IdEstado = objSearchOst.IdEstado,
                audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession)
            };
            try
            {
                objListOSTResponseColiving = Claro.Web.Logging.ExecuteMethod<ColivingService.ListOSTResponseColiving>(
                () => { return oColivingService.GetListOST_Legacy(objListOSTRequestColiving); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListOSTRequestColiving.audit.Session, objListOSTRequestColiving.audit.transaction, ex.Message);
            }
            return objListOSTResponseColiving;
        }



        /// <summary>
        /// Método que obtiene el listado de la OST de ONE
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="SearchOstModel">SearchOstModel</param>
        /// <returns>JsonResult</returns> 
        public ListOSTResponseColiving GetListOST_One(String strIdSession, SearchOstModel objSearchOst)
        {
            ListOSTRequestColiving objListOSTRequestColiving = null;
            ListOSTResponseColiving objListOSTResponseColiving = null;
            objListOSTRequestColiving = new ListOSTRequestColiving()
            {
                IdBusca = objSearchOst.IdBusca,
                IdCAC = objSearchOst.IdCAC,
                IdCriterio = objSearchOst.IdCriterio,
                IdEstado = objSearchOst.IdEstado,
                audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession)
            };
            objListOSTRequestColiving.audit.ipAddress = App_Code.Common.GetApplicationIpServer();
            try
            {
                objListOSTResponseColiving = Claro.Web.Logging.ExecuteMethod<ColivingService.ListOSTResponseColiving>(
                () => { return oColivingService.GetListOST_One(objListOSTRequestColiving); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListOSTRequestColiving.audit.Session, objListOSTRequestColiving.audit.transaction, ex.Message);
            }
            return objListOSTResponseColiving;
        }


        /// <summary>
        /// Método que obtiene el listado de la OST detallado
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="SearchOstModel">SearchOstModel</param>
        /// <returns>JsonResult</returns> 
        public ListOSTResponseColiving GetListOSTDetails_One(String strIdSession, SearchOstModel objSearchOst)
        {

            ListOSTRequestColiving objListOSTRequestColiving = null;
            ListOSTResponseColiving objListOSTResponseColiving = null;
            ListOSTResponseColiving objListOSTResponseColiving2 = null;
            objListOSTRequestColiving = new ListOSTRequestColiving()
            {
                IdBusca = objSearchOst.IdBusca,
                IdCAC = objSearchOst.IdCAC,
                IdCriterio = objSearchOst.IdCriterio,
                IdEstado = objSearchOst.IdEstado,
                audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession)
            };
            objListOSTRequestColiving.audit.ipAddress = App_Code.Common.GetApplicationIpServer();
            try
            {
                objListOSTResponseColiving = Claro.Web.Logging.ExecuteMethod<ColivingService.ListOSTResponseColiving>(
                () => { return oColivingService.GetListOST_One(objListOSTRequestColiving); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListOSTRequestColiving.audit.Session, objListOSTRequestColiving.audit.transaction, ex.Message);
            }

            if (objListOSTResponseColiving != null) {
                if (objListOSTResponseColiving.CodeResponse == Constants.ZeroNumber)
                {

                    for (int i = 0; i < objListOSTResponseColiving.TechnicalServicesOSTType.Count; i++)
                    {
                        objListOSTRequestColiving = new ListOSTRequestColiving()
                        {
                            IdBusca = objListOSTResponseColiving.TechnicalServicesOSTType[i].idOst,
                            audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession)
                        };
                        objListOSTRequestColiving.audit.ipAddress = App_Code.Common.GetApplicationIpServer();
                        try
                        {
                            objListOSTResponseColiving2 = Claro.Web.Logging.ExecuteMethod<ColivingService.ListOSTResponseColiving>(
                            () => { return oColivingService.GetListOSTDetails_One(objListOSTRequestColiving); });
                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(objListOSTRequestColiving.audit.Session, objListOSTRequestColiving.audit.transaction, string.Format("Error: no se pudo obtener la Marca y el Modelo, IDOST: {0}", objListOSTResponseColiving.TechnicalServicesOSTType[i].idOst));
                            Claro.Web.Logging.Error(objListOSTRequestColiving.audit.Session, objListOSTRequestColiving.audit.transaction, ex.Message);
                        }
                        if (objListOSTResponseColiving2.CodeResponse == Constants.ZeroNumber)
                        {
                            if (objListOSTResponseColiving2.TechnicalServicesOSTType.Count > 0)
                            {
                                objListOSTResponseColiving.TechnicalServicesOSTType[i].Marca = objListOSTResponseColiving2.TechnicalServicesOSTType[0].Marca;
                                objListOSTResponseColiving.TechnicalServicesOSTType[i].Modelo = objListOSTResponseColiving2.TechnicalServicesOSTType[0].Modelo;
                            }
                        }
                        else {
                            Claro.Web.Logging.Error(objListOSTRequestColiving.audit.Session, objListOSTRequestColiving.audit.transaction, string.Format("Error: {0}", objListOSTResponseColiving2.DescriptionResponse));
                        }

                    }
                }
            }

            return objListOSTResponseColiving;
        }

        /// <summary>
        /// JsonResult ListServiceCountCurrent que consulta las líneas activas, lineas inactivas.
        /// </summary>
        /// <param name="strCuenta">strCuenta</param>
        /// <param name="strActivaInactiva">strActivaInactiva</param>
        /// <returns>Devuelve objeto en JSON de lista de lineas activa, inactiva</returns> 
        public JsonResult ListServiceCountCurrent(string strCuenta, string strActivaInactiva)
        {

            var estatusLinea = KEY.AppSettings("strKeySubscriptionStatus").Split('|');
            var estatusInactivo = KEY.AppSettings("strKeySubscriptionStatus").Split('|');

            var listAccountSubscription = new List<AccountSubscription>();
            if (strActivaInactiva == estatusLinea[1].Split(';')[1])
            {
                listAccountSubscription = Config.model.ListPostPaidService.Where(x => x.BillingAccountId == strCuenta).FirstOrDefault().SubscriptionActive.ToList();
            }
            if (strActivaInactiva == estatusLinea[3].Split(';')[1])
            {
                listAccountSubscription = Config.model.ListPostPaidService.Where(x => x.BillingAccountId == strCuenta).FirstOrDefault().SubscriptionInactive.ToList();
            }
            AccountCustomerModel accountCustomerModel = null;
            accountCustomerModel = new AccountCustomerModel()
            {
                ListSuscription = listAccountSubscription
            };
            return Json(new { data = accountCustomerModel });
        }
        #endregion

        #region ValidarFullStack
        /// <summary>
        /// JsonResult VerificarCuentaFullStack que valida si el cliente tiene una cuenta en FULLSTACK
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="objSearchType">objSearchType</param>
        /// <returns></returns> 
        public JsonResult VerificarCuentaFullStack(string strIdSession, SearchTypeModel objSearchType = null)
        {
            var productoOne = false;
            var objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
            try
            {
            if (objSearchType != null)
            {
                var objSearchCustomerResponse = SearchCustomerService(strIdSession, objSearchType);
                if (objSearchCustomerResponse != null)
                {
                    if (objSearchCustomerResponse.Status == Claro.Constants.NumberZero)
                    {
                        productoOne = true;
                    }
                }
            }
                string strDocumentType = objSearchType.DocumentTypeDescription + ": ";
                string strDocumentNumber = objSearchType.DocumentNumber;
                string strConsultDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string strText = KEY.AppSettings("strConstMsgValidateSpecialCasesWeb") + strDocumentType + strDocumentNumber + Claro.SIACU.Constants.DoubleScript + Claro.SIACU.Constants.DateHours + strConsultDate;
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strDocumentNumber, KEY.AppSettings("strAudiTraValidateSpecialCasesWeb"), strText); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
            }
            return Json(new { data = productoOne });
        }
        /// <summary>
        /// JsonResult VerificarCuentaFullStack_RS valida si el cliente tiene una cuenta en FULLSTACK
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="objSearchType">objSearchType</param>
        /// <returns></returns> 
        public JsonResult VerificarCuentaFullStack_RS(string strIdSession, SearchTypeModel objSearchType = null)
        {
            var productoOne = false;
            if (objSearchType != null)
            {
                var objResponse = RetrieveSubscriptionService(strIdSession, objSearchType);
                if (objResponse != null)
                {
                    if (objResponse.Status == Claro.Constants.NumberZero)
                    {
                        productoOne = true;
                    }
                }
            }
            return Json(new { data = productoOne });
        }
        #endregion



        #region Services

        /// <summary>
        /// Método que consulta el servicio SearchCustomer.
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="objSearchType">objSearchType</param>
        /// <returns>objSearchCustomerResponse</returns> 
        public SearchCustomerResponseColiving SearchCustomerService(string strIdSession, SearchTypeModel objSearchType)
        {
            SearchCustomerRequestColiving objSearchCustomerRequest = null;
            SearchCustomerResponseColiving objSearchCustomerResponse = null;

            objSearchCustomerRequest = new SearchCustomerRequestColiving()
            {
                Msisdn = objSearchType.LineNumber,
                CustomerId = objSearchType.CustomerId,
                DocumentType = objSearchType.DocumentType,
                DocumentNumber = objSearchType.DocumentNumber,
                audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession)
            };
            objSearchCustomerRequest.audit.ipAddress = App_Code.Common.GetApplicationIpServer();
            try
            {
                objSearchCustomerResponse = Claro.Web.Logging.ExecuteMethod<ColivingService.SearchCustomerResponseColiving>(
                () => { return oColivingService.GetSearchCustomer(objSearchCustomerRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSearchCustomerRequest.audit.Session, objSearchCustomerRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException("" + ex.Message);
            }
            return objSearchCustomerResponse;
        }
        /// <summary>
        /// Método que consulta el servicio RetrieveSubscription
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="objSearchType">objSearchType</param>
        /// <returns>objSearchCustomerResponse</returns> 
        public RetrieveSubscriptionResponse RetrieveSubscriptionService(string strIdSession, SearchTypeModel objSearchType)
        {

            RetrieveSubscriptionRequest objPostpaidLinesRequest = null;
            RetrieveSubscriptionResponse objSearchCustomerResponse = null;
            objPostpaidLinesRequest = new RetrieveSubscriptionRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession),
                customerId = objSearchType.CustomerId,
                DocumentNumber = objSearchType.DocumentNumber,
                DocumentType = objSearchType.DocumentType,
                serviceIdentifier = objSearchType.LineNumber
            };
            objPostpaidLinesRequest.audit.ipAddress = App_Code.Common.GetApplicationIpServer();
            try
            {
                objSearchCustomerResponse = Claro.Web.Logging.ExecuteMethod<ColivingService.RetrieveSubscriptionResponse>(
                () => { return oColivingService.GetRetrieveSubscriptions(objPostpaidLinesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objPostpaidLinesRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException("");
            }
            return objSearchCustomerResponse;
        }
        /// <summary>
        /// Método que consulta el servicio ConsultaLineaCuenta para validar si la linea se encuentra migrada.
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="objSearchType">strType</param>
        /// /// <param name="objSearchType">strValue</param>
        /// <returns></returns> 
        public ConsultaLineaResponse ConsultaLineaCuenta(string strIdSession, string strType, string strValue)
        {
            ConsultaLineaRequest objConsultaLineaRequest = null;
            ConsultaLineaResponse objConsultaLineaResponse = null;
            objConsultaLineaRequest = new ConsultaLineaRequest()
            {
                Type = strType,
                Value = strValue,
                audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession)
            };
            try
            {
                objConsultaLineaResponse = Claro.Web.Logging.ExecuteMethod<ConsultaLineaResponse>(
                () => { return oColivingService.ConsultarLineaCuenta(objConsultaLineaRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objConsultaLineaRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException("");
            }
            return objConsultaLineaResponse;
        }
        /// <summary>
        /// Método que consulta el servicio ObtenerDatosClienteService para obtener los datos cuando la linea/cuenta no se encuentra migrada.
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="objSearchType">objSearchType</param>
        /// <returns></returns> 
        public ObtenerDatosClienteResponseColiving ObtenerDatosClienteService(string strIdSession, SearchTypeModel objSearchType)
        {
            ObtenerDatosClienteRequestColiving objObtenerDatosRequest = null;
            ObtenerDatosClienteResponseColiving objObtenerDatosResponse = null;

            objObtenerDatosRequest = new ObtenerDatosClienteRequestColiving()
            {
                CustomerId = objSearchType.CustomerId,
                LineNumber = objSearchType.LineNumber,
                DocumentNumber = objSearchType.DocumentNumber,
                DocumentType = objSearchType.DocumentType,
                audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession)
            };
            objObtenerDatosRequest.audit.ipAddress = App_Code.Common.GetApplicationIpServer();
            try
            {
                objObtenerDatosResponse = Claro.Web.Logging.ExecuteMethod<ColivingService.ObtenerDatosClienteResponseColiving>(
                () => { return oColivingService.GetObtenerDatosCliente(objObtenerDatosRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, "", ex.Message);
                throw new Claro.MessageException("");
            }
            return objObtenerDatosResponse;
        }
        #endregion
        /// <summary>
        /// Método que inserta la auditoria cuando se redirecciona a venta .
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="objSearchType">objCustomer</param>
        /// <returns></returns> 
        public ActionResult SIACU_Criteria_Sale(string strIdSession, SearchCustomerModel objCustomer)
        {
            ViewBag.strPrepagoOlo = KEY.AppSettings("strPrepagoOlo").ToString();
            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
            try
            {
                string strDocumentType = objCustomer.DocumentTypeDescription + ": ";
                string strDocumentNumber = objCustomer.DocumentNumber;
                string strConsultDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string strText = KEY.AppSettings("strConstMsgCriteriosVenta") + Claro.SIACU.Constants.DoubleScript + Claro.SIACU.Constants.DateHours + strConsultDate + Claro.SIACU.Constants.DoubleScript + strDocumentType + strDocumentNumber;
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strDocumentNumber, KEY.AppSettings("strAudiTraPantallaCriteriosVenta"), strText); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
            }
            return PartialView();
        }
        /// <summary>
        /// Clase estática que contiene los datos 
        /// </summary>
        /// <param name="strIdSession">strIdSession</param>
        /// <param name="objSearchType">objSearchType</param>
        /// <returns>Devuelve la búsqueda de Cliente y susbscripción </returns> 
        public static class Config
        {
            public static DocumentCustomerModel model = null;
        }
    }
}