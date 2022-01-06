using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.ServicePostpaid;
using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models;
using Claro.SIACU.Web.WebApplication.DashboardService;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HELPER_DATA = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.CustomerHelper;
using KEY = Claro.ConfigurationManager;
using System.Linq;


namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly DashboardService.DashboardServiceClient oServiceDashboard = new DashboardService.DashboardServiceClient();
        public ActionResult Products()
        {
            return PartialView();
        }

        public ActionResult AlertCustomer()
        {
            return View();
        }

        public ActionResult CustomersDocument()
        {
            return PartialView();
        }
        public ActionResult ValidateCustomer()
        {
            return PartialView();
        }

        public ActionResult CustomersNames(string strIdSession)
        {
            DataCustomerModel objDataCustomerModel;
            string strTrasaction = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession).transaction;
            try
            {
                objDataCustomerModel = new DataCustomerModel()
                {
                    Namelength = KEY.AppSettings("strNamelength"),
                    LastNamelength = KEY.AppSettings("strLastNamelength")
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTrasaction, ex.Message);
                throw new Claro.MessageException(strTrasaction);
            }
            return PartialView(objDataCustomerModel);
        }

        public ActionResult CustomersBusinessNames()
        {
            return View();
        }

        public FileResult DownloadExcel(string strPath, string strNewfileName)
        {
            return File(strPath, "application/vnd.ms-excel", strNewfileName);
        }

        public ActionResult ProductDetails(string strIdSession, string strCustomerId, string strCodeProduct, string strIdPlan, string strState, string strProductType)
        {

            PostpaidLinesResponseDashboard objPostpaidLinesResponse;
            PostpaidLinesRequestDashboard objPostpaidLinesRequest = null;
            PostpaidProductDetailModel oPostpaidProductModel = new PostpaidProductDetailModel()
            {
                StateLines = strState,
                TypeProduct = strProductType
            };

            try
            {
                objPostpaidLinesRequest = new PostpaidLinesRequestDashboard()
                {
                    CustomerId = strCustomerId,
                    CodeProduct = strCodeProduct,
                    IdPlan = strIdPlan,
                    State = strState,
                    Origin = KEY.AppSettings("strOrigenDatos"),
                    ProductType = strProductType,
                    audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession)
                };
                objPostpaidLinesResponse = Claro.Web.Logging.ExecuteMethod<DashboardService.PostpaidLinesResponseDashboard>(
                    () => { return new DashboardService.DashboardServiceClient().GetPostpaidLines(objPostpaidLinesRequest); });
            }
            catch (Exception ex)
            {
                objPostpaidLinesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objPostpaidLinesRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objPostpaidLinesRequest.audit.transaction);
            }
            if (objPostpaidLinesResponse != null)
            {
                foreach (var item in objPostpaidLinesResponse.ListDetailProduct)
                {
                    oPostpaidProductModel.listProducDetailPost.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.PostpaidProductDetail.DetailProduct()
                    {
                        CoId = item.CoId,
                        State = item.Estado.ToUpper(),
                        DateActive = item.FechaActivacion,
                        Telephone = item.Telefono
                    });
                }
            }
            return PartialView(oPostpaidProductModel);
        }

        public JsonResult FillInstant(string strIdSession, string strPhone, string strAccount, string strContract, string strOriginType)
        {
            string strDataSearch = "";
            string strTypePhone = "";
            int intRegistros = Claro.Constants.NumberZero;
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.Instant.Instant> listInstant;
            DashboardService.InstantsResponseDashboard objInstantsResponseDashboard;

            switch (strOriginType)
            {
                case Claro.SIACU.Constants.PostpaidMajuscule:
                    strDataSearch = strPhone + "|" + strAccount;
                    strTypePhone = Claro.Constants.NumberTwelveString;
                    break;
                case Claro.SIACU.Constants.HFC:
                    strDataSearch = strPhone + "|" + strAccount + "|" + strContract;
                    strTypePhone = Claro.Constants.NumberTwelveString;
                    break;
                case Claro.SIACU.Constants.PrepaidMajuscule:
                    strDataSearch = strPhone;
                    strTypePhone = Claro.Constants.NumberTenString;
                    break;
                default:
                    break;
            }
            DashboardService.InstantsRequestDashboard objInstantsRequestDashboard = new DashboardService.InstantsRequestDashboard
            {
                DataSearch = strDataSearch,
                TypePhone = strTypePhone,
                OriginType = strOriginType,
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession)
            };

            try
            {
                objInstantsResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.InstantsResponseDashboard>(
                    () => { return new DashboardService.DashboardServiceClient().GetInstants(objInstantsRequestDashboard); });
            }
            catch (Exception ex)
            {
                objInstantsResponseDashboard = null;
                Claro.Web.Logging.Error(strIdSession, objInstantsRequestDashboard.audit.transaction, ex.Message);
                throw new Claro.MessageException(objInstantsRequestDashboard.audit.transaction);
            }
            listInstant = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.Instant.Instant>();

            if (objInstantsResponseDashboard.ListInstant != null)
            {

                foreach (DashboardService.Instant item in objInstantsResponseDashboard.ListInstant)
                {
                    listInstant.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.Instant.Instant()
                    {
                        IdInstant = item.ID_INSTANTANEA,
                        Validity = item.VIGENCIA,
                        Description = item.DESCRIPCION,
                        DateValidityStartFormat = item.FECHA_VIGENCIA_INICIO.ToString("dd/MM/yyyy hh:mm tt"),
                        DateValidityEndFormat = item.FECHA_VIGENCIA_FIN.ToString("dd/MM/yyyy hh:mm tt"),
                        Color = item.COLOR
                    });
                    intRegistros++;
                }
            }

            InstantModel objDataInstantModel = new InstantModel()
            {
                listInstant = listInstant,
                NumberRegisters = intRegistros.ToString()
            };

            return Json(objDataInstantModel);
        }
        public JsonResult FillInstantPrepaid(string strIdSession, string strPhone, string strOriginType)
        {
            string strTypePhone = Claro.Constants.NumberTenString;
            int intRegistros = Claro.Constants.NumberZero;
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.Instant.Instant> listInstant;
            DashboardService.InstantsResponseDashboard objInstantsResponseDashboard;
            DashboardService.InstantsRequestDashboard objInstantsRequestDashboard = new DashboardService.InstantsRequestDashboard
            {
                DataSearch = strPhone,
                TypePhone = strTypePhone,
                OriginType = strOriginType,
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession)
            };

            try
            {
                objInstantsResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.InstantsResponseDashboard>(
                    () => { return new DashboardService.DashboardServiceClient().GetInstants(objInstantsRequestDashboard); });
            }
            catch (Exception ex)
            {
                objInstantsResponseDashboard = null;
                Claro.Web.Logging.Error(strIdSession, objInstantsRequestDashboard.audit.transaction, ex.Message);
                throw new Claro.MessageException(objInstantsRequestDashboard.audit.transaction);
            }
            listInstant = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.Instant.Instant>();
            if (objInstantsResponseDashboard.ListInstant != null)
            {
                foreach (DashboardService.Instant item in objInstantsResponseDashboard.ListInstant)
                {
                    listInstant.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.Instant.Instant()
                    {
                        IdInstant = item.ID_INSTANTANEA,
                        Validity = item.VIGENCIA,
                        Description = item.DESCRIPCION,
                        DateValidityStartFormat = item.FECHA_VIGENCIA_INICIO.ToString("dd/MM/yyyy hh:mm tt"),
                        DateValidityEndFormat = item.FECHA_VIGENCIA_FIN.ToString("dd/MM/yyyy hh:mm tt"),
                        Color = item.COLOR,
                    });
                    intRegistros++;

                }
            }
            InstantModel objDataInstantModel = new InstantModel()
            {
                TextFilter = KEY.AppSettings("strTextFilter"),
                listInstant = listInstant,
                NumberRegisters = intRegistros.ToString(),
            };

            return Json(objDataInstantModel);
        }
        public ActionResult Instants(InstantModel objInstantModel)
        {
            if (objInstantModel.listInstant == null)
            {
                objInstantModel.listInstant = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.Instant.Instant>();
            }
            return PartialView(objInstantModel);
        }

        public ActionResult InstantsPrepaid(InstantModel objInstantModel)
        {
            if (objInstantModel.listInstant == null)
            {
                objInstantModel.listInstant = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.Instant.Instant>();
            }
            return View(objInstantModel);
        }

        [HttpGet]
        public JsonResult Customer(string strIdSession, string strTypeSearch, string strValueSearch)
        {

            CustomerInfoResponseDashboard oGetCustomerInfoResponse;
            List<HELPER_DATA.Customer> oListCustomerModel = null;
            CustomerInfoRequestDashboard oGetCustomerInfoRequest = new CustomerInfoRequestDashboard()
            {
                SearchType = strTypeSearch,
                SearchValue = strValueSearch,
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession)
            };

            try
            {
                oGetCustomerInfoResponse = Claro.Web.Logging.ExecuteMethod<DashboardService.CustomerInfoResponseDashboard>(
                    () => { return new DashboardService.DashboardServiceClient().GetCustomerInfo(oGetCustomerInfoRequest); });
            }
            catch (Exception ex)
            {
                oGetCustomerInfoResponse = null;
                Claro.Web.Logging.Error(oGetCustomerInfoRequest.audit.Session, oGetCustomerInfoRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(oGetCustomerInfoRequest.audit.transaction);
            }

            if (oGetCustomerInfoResponse != null && oGetCustomerInfoResponse.ListPerson.Count == 1)
            {

                oListCustomerModel = new List<HELPER_DATA.Customer>();
                oListCustomerModel.Add(new HELPER_DATA.Customer()
                {
                    Surnames = oGetCustomerInfoResponse.ListPerson[0].APELLIDOS,
                    DocumentIdentity = oGetCustomerInfoResponse.ListPerson[0].NRO_DOC,
                    Names = oGetCustomerInfoResponse.ListPerson[0].NOMBRES,
                    RazonSocial = oGetCustomerInfoResponse.ListPerson[0].RAZON_SOCIAL,
                    DocumentType = oGetCustomerInfoResponse.ListPerson[0].TIPO_DOC,
                });
            }
            DataCustomerModel oCustomerModel = new DataCustomerModel()
            {
                listDataCustomerModel = oListCustomerModel
            };

            return Json(new { data = oCustomerModel }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostpaidProducts(string strIdSession, string strDocumentType, string strDocumentIdentity)
        {

            PostpaidProductsResponseDashboard objPostpaidProductsResponse;
            PostpaidProductModel objPostpaidProductModel = null;
            List<ServicePostpaid> oListProductModel = null;
            ServicePostpaid oServicePostpaidModel;
            PostpaidProductsRequestDashboard objPostpaidProductsRequest = new PostpaidProductsRequestDashboard()
            {
                DocumentType = strDocumentType,
                DocumentIdentity = strDocumentIdentity,
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession)

            };
            try
            {
                objPostpaidProductsResponse = Claro.Web.Logging.ExecuteMethod<DashboardService.PostpaidProductsResponseDashboard>(
                    () => { return new DashboardService.DashboardServiceClient().GetPostpaidProducts(objPostpaidProductsRequest); });
            }
            catch (Exception ex)
            {
                objPostpaidProductsResponse = null;
                Claro.Web.Logging.Error(strIdSession, objPostpaidProductsRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objPostpaidProductsRequest.audit.transaction);
            }
            if (objPostpaidProductsResponse != null)
            {

                oListProductModel = new List<ServicePostpaid>();
                foreach (var item in objPostpaidProductsResponse.ListProduct)
                {
                    if (string.IsNullOrEmpty(item.IdPlan)) item.IdPlan = "";
                    if (string.IsNullOrEmpty(item.TipoProducto)) item.TipoProducto = "";

                    oServicePostpaidModel = new ServicePostpaid()
                    {
                        Id = item.id,
                        IdPlan = item.IdPlan,
                        TypeProduct = item.TipoProducto,
                        CodProduct = item.CodigoProducto,
                        Product = item.Producto,
                        Account = item.Cuenta,
                        CodClient = item.CodigoCliente,
                        TypeClient = item.TipoCliente,
                        DateAccountActivation = item.FechaActivacionCuenta.ToString(),
                        NumberServicesActive = item.NroServiciosActivos,
                        NumberServicesNotActive = item.NroServiciosNoActivos
                    };

                    oListProductModel.Add(oServicePostpaidModel);
                }
                objPostpaidProductModel = new PostpaidProductModel()
                {
                    DataOrigin = objPostpaidProductsResponse.DataOrigin,
                    olistProducPost = oListProductModel
                };
            }
            else
            {
                objPostpaidProductModel = null;
            }
            return Json(new { data = objPostpaidProductModel });
        }

        public JsonResult PrepaidProducts(string strIdSession, string strDocumentType, string strDocumentIdentity, string strState)
        {
            PrepaidProductsResponseDashboard objPrepaidProductsResponse;
            List<PrepaidProductModel> oListPrepaidProductModel = null;
            PrepaidProductsRequestDashboard objPrepaidProductsRequest = new PrepaidProductsRequestDashboard()
            {
                DocumentType = strDocumentType,
                DocumentIdentity = strDocumentIdentity,
                State = strState,
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession)
            };

            try
            {
                objPrepaidProductsResponse = Claro.Web.Logging.ExecuteMethod<DashboardService.PrepaidProductsResponseDashboard>(
                    () => { return new DashboardService.DashboardServiceClient().GetPrepaidProducts(objPrepaidProductsRequest); });
            }
            catch (Exception ex)
            {
                objPrepaidProductsResponse = null;
                Claro.Web.Logging.Error(strIdSession, objPrepaidProductsRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objPrepaidProductsRequest.audit.transaction);
            }
            if (objPrepaidProductsResponse != null)
            {
                oListPrepaidProductModel = new List<PrepaidProductModel>();
                foreach (var item in objPrepaidProductsResponse.ListProduct)
                {
                    oListPrepaidProductModel.Add(new PrepaidProductModel()
                    {
                        TypeProduct = item.TipoProducto,
                        Telephone = item.Telefono,
                        State = item.Estado,
                        DateActive = item.FechaActivacion.ToString(),
                        ObjId = item.ObjId,
                        Id = item.Id
                    });
                }
            }
            else
            {
                oListPrepaidProductModel = null;
            }

            return Json(new { data = oListPrepaidProductModel });
        }

        public JsonResult CustomerList(string strIdSession, string strSearch, string strLastName)
        {
            CustomerNameRequestDashboard objCustomerNameRequest = new CustomerNameRequestDashboard();
            CustomerNameResponseDashboard objCustomerNameResponse;
            DataCustomerModel oDatosGenerales = new DataCustomerModel();

            if (!string.IsNullOrEmpty(strSearch))
            {
                string strTextOlo, numeDoc, key;
                if (strLastName != "")
                {
                    objCustomerNameRequest.SearchType = Claro.Constants.NumberTwoString;
                    objCustomerNameRequest.SearchValue = strSearch + "|" + strLastName;
                    strTextOlo = string.Format("{0}: {1} {2}", Tools.Utils.Constants.Dates, strSearch, strLastName);
                    numeDoc = strSearch + "|" + strLastName;
                    key = KEY.AppSettings("strAudiTraNombre");
                }
                else
                {
                    objCustomerNameRequest.SearchType = Claro.Constants.NumberThreeString;
                    objCustomerNameRequest.SearchValue = strSearch;
                    strTextOlo = string.Format("{0}: {1}", Tools.Utils.Constants.BusinessName, strSearch);
                    numeDoc = strSearch;
                    key = KEY.AppSettings("strAudiTraRazonSocial");
                }

                OptionalObject objOptionalObject = new OptionalObject()
                {
                    name = "",
                    value = ""
                };

                objCustomerNameRequest.ListOptionalObject = objOptionalObject;

                try
                {
                    objCustomerNameRequest.audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
                    objCustomerNameResponse = Claro.Web.Logging.ExecuteMethod<DashboardService.CustomerNameResponseDashboard>(
                        () => { return new DashboardService.DashboardServiceClient().GetCustomerName(objCustomerNameRequest); });
                }
                catch (Exception ex)
                {
                    objCustomerNameResponse = null;
                    Claro.Web.Logging.Error(strIdSession, objCustomerNameRequest.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objCustomerNameRequest.audit.transaction);
                }

                string strTraCod = string.Empty;
                string strTypeSearch = string.Empty;
                string strTextSearch = string.Empty;
                string strvalue = objCustomerNameRequest.SearchType;

                if (objCustomerNameResponse != null)
                {
                    switch (strvalue)
                    {
                        case Claro.Constants.NumberTwoString:
                            strTraCod = KEY.AppSettings("strAudiTraCodBusquedaNombres");
                            strTypeSearch = Claro.SIACU.Constants.Dates;
                            strTextSearch = objCustomerNameRequest.SearchValue;
                            break;
                        case Claro.Constants.NumberThreeString:
                            strTraCod = KEY.AppSettings("strAudiTraCodBusquedaRzSocial");
                            strTypeSearch = Claro.SIACU.Constants.BusinessName;
                            strTextSearch = objCustomerNameRequest.SearchValue;
                            break;
                    }
                    SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
                    try
                    {
                        string strText = strTypeSearch + Claro.SIACU.Constants.DoubleScript + strTextSearch;
                        Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, "", strTraCod, strText); });
                        Claro.Web.Logging.ExecuteMethod<string>(() =>
                        {
                            return App_Code.Common.InsertAudit(objaudit, numeDoc, key, strTextOlo);
                        });
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
                    }
                }

                if (objCustomerNameResponse.ListPerson != null)
                {
                    List<HELPER_DATA.Customer> ListDatosGenerales = new List<HELPER_DATA.Customer>();

                    List<Person> Person = objCustomerNameResponse.ListPerson.GroupBy(p => new { p.NRO_DOC, p.TIPO_DOC })
                       .Select(g => g.First())
                       .ToList();


                    foreach (Person item in Person)
                    {
                        if ((String.IsNullOrEmpty(item.NRO_DOC)) || (String.IsNullOrEmpty(item.TIPO_DOC)) || (String.IsNullOrEmpty(item.DESCRIPCION))) { continue; }
                        else
                        {
                            ListDatosGenerales.Add(new HELPER_DATA.Customer()
                            {
                                Names = item.DESCRIPCION,
                                DocumentType = item.TIPO_DOC,
                                DocumentIdentity = item.NRO_DOC,
                                RazonSocial = item.RAZON_SOCIAL
                            });

                        }

                    }
                    oDatosGenerales.listDataCustomerModel = ListDatosGenerales;
                }
            }

            return Json(new { data = oDatosGenerales });
        }

        public JsonResult CustomerValidate(string strIdSession, string strSearchType, string strSearchValue)
        {
            CustomerInfoResponseDashboard oGetCustomerInfoResponse;
            DataCustomerModel oDataCustomerModel = null;
            CustomerInfoRequestDashboard oGetCustomerInfoRequest = new CustomerInfoRequestDashboard()
            {
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession),
                SearchType = strSearchType,
                SearchValue = strSearchValue
            };
            try
            {
                oGetCustomerInfoResponse = Claro.Web.Logging.ExecuteMethod<DashboardService.CustomerInfoResponseDashboard>(
                    () => { return new DashboardService.DashboardServiceClient().GetCustomerInfo(oGetCustomerInfoRequest); });
            }
            catch (Exception ex)
            {
                oGetCustomerInfoResponse = null;
                Claro.Web.Logging.Error(strIdSession, oGetCustomerInfoRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(oGetCustomerInfoRequest.audit.transaction);
            }

            if (oGetCustomerInfoResponse != null)
            {
                oDataCustomerModel = new DataCustomerModel();

                List<Person> Person = oGetCustomerInfoResponse.ListPerson.GroupBy(p => new { p.NRO_DOC, p.TIPO_DOC })
                        .Select(g => g.First())
                        .ToList();
                foreach (var item in Person)
                {
                    if ((String.IsNullOrEmpty(item.NRO_DOC)) || (String.IsNullOrEmpty(item.TIPO_DOC)) || (String.IsNullOrEmpty(item.DESCRIPCION))) { continue; }
                    else
                    {
                        oDataCustomerModel.listDataCustomerModel.Add(new HELPER_DATA.Customer()
                        {
                            DocumentIdentity = item.NRO_DOC,
                            Names = item.DESCRIPCION,
                            RazonSocial = item.RAZON_SOCIAL,
                            DocumentType = item.TIPO_DOC
                        });
                    }

                }

                SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
                try
                {
                    string strTraCod = KEY.AppSettings("strAudiTraCodBusquedaDNI");
                    string strTypeSearch = Claro.SIACU.Constants.DNINumber;
                    string strText = strTypeSearch + Claro.SIACU.Constants.DoubleScript + strSearchValue;
                    string strTextOlo = string.Format("{0}: {1}", Tools.Utils.Constants.DNINumber, strSearchValue);
                    Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strSearchValue, strTraCod, strText); });
                    Claro.Web.Logging.ExecuteMethod<string>(() =>
                    {
                        return App_Code.Common.InsertAudit(objaudit, strSearchValue, KEY.AppSettings("strAudiTraDNINumber"), strTextOlo);
                    });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
                }
            }
            else
            {
                oDataCustomerModel = null;
            }
            return Json(new { data = oDataCustomerModel });
        }

      
        public JsonResult GetStateEquipo(string strLinea, string strIdSession)
        {
            StateEquipoResponse response = new StateEquipoResponse();
            StateEquipoRequest request = new StateEquipoRequest();
            try
            {
                request = new StateEquipoRequest();
                request.audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
                request.AuditService = new Audit();
                request.MessageRequest = new StateEquipoMessageRequest();
                request.MessageRequest.Header = new StateEquipoHeaderRequest();
                request.MessageRequest.Header.HeaderRequest = getHeaderRequest(KEY.AppSettings("strOperationconsultaSitic"));
                request.MessageRequest.Body = new StateEquipoBodyRequest();
                request.MessageRequest.Body.PI_LINEA = strLinea;

                response = oServiceDashboard.GetStateEquipo(request);
                if (response != null ? (response.MessageResponse != null ? (response.MessageResponse.Body != null ? (response.MessageResponse.Body.PO_CODERROR.Equals("-99") ? false : true) : false) : false) : false)
                {
                    Claro.Web.Logging.Info(request.audit.Session, request.audit.transaction, response.MessageResponse.Body.PO_CODERROR);
                    Claro.Web.Logging.Info(request.audit.Session, request.audit.transaction, response.MessageResponse.Body.PO_MSJERROR);
                    Claro.Web.Logging.Info(request.audit.Session, request.audit.transaction, response.MessageResponse.Body.PO_ESTADO);

                }
                else
                {
                    response = new StateEquipoResponse();
                    response.MessageResponse = new StateEquipoMessageResponse();
                    response.MessageResponse.Body = new StateEquipoBodyResponse();

                    response.MessageResponse.Body.PO_CODERROR = "-99";
                    response.MessageResponse.Body.PO_ESTADO = "Response Null";
                    response.MessageResponse.Body.PO_MSJERROR = "Response null";
                    Claro.Web.Logging.Error(request.audit.Session, request.audit.transaction, response.MessageResponse.Body.PO_MSJERROR);

                }

            }
            catch (Exception ex)
            {
                response = new StateEquipoResponse();
                response.MessageResponse = new StateEquipoMessageResponse();
                response.MessageResponse.Body = new StateEquipoBodyResponse();

                response.MessageResponse.Body.PO_CODERROR = "-99";
                response.MessageResponse.Body.PO_ESTADO = "Response Null";
                response.MessageResponse.Body.PO_MSJERROR = "Response null";
                Claro.Web.Logging.Error(request.audit.Session, request.audit.transaction, ex.Message);
            }


            return Json(new { data = response.MessageResponse.Body });
        }

  public JsonResult getKeyValue(string strKeyName, string strIdSession)
        {
            string strValue = "";
            try
            {
                strValue = KEY.AppSettings(strKeyName);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, ex.Message);
            }
            return Json(new { data = strValue });
        }
        public JsonResult Fraccionamiento(string strUrl, string strUser)
        {
            string Path = string.Empty;
            try
            {
                string strKey = KEY.AppSettings("strFraccion");
                string userEncript = Claro.SIACU.Web.WebApplication.App_Code.Common.Encrypt(strUser, strKey);
                string param = KEY.AppSettings("strParamFRC");
                Path = string.Concat(strUrl, param,userEncript);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("", "Fraccionamiento", ex.Message);

            }
            return Json(new { data = Path });
        }

        public DashboardService.HeaderRequest getHeaderRequest(string operation)
        {
            return new DashboardService.HeaderRequest()
            {
                consumer = KEY.AppSettings("consumer"),
                country = KEY.AppSettings("country"),
                dispositivo = KEY.AppSettings("dispositivoSitic"),
                language = KEY.AppSettings("language"),
                modulo = KEY.AppSettings("moduloSitic"),
                msgType = KEY.AppSettings("msgType"),
                operation = operation,
                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                system = KEY.AppSettings("systemSitic"),
                timestamp = DateTime.Now.ToString("o"),
                userId = App_Code.Common.CurrentUser,
                wsIp = App_Code.Common.GetApplicationIp()
            };
        }
    }
}