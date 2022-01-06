using Claro.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using HELPER = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid;
using KEY = Claro.ConfigurationManager;
using PINPUK_MODEL = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataAccount.AccountPinPukModel;
using RELATIONPLAN_MODEL = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataAccount.AccountRelationPlanModel;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class PostpaidDataAccountController : Controller
    {
        private readonly Claro.Helpers.ExcelHelper oExcelHelper = new Claro.Helpers.ExcelHelper();
        private readonly PostpaidService.PostpaidServiceClient objPostpaidService = new PostpaidService.PostpaidServiceClient();

        public ActionResult AccountAnnotationList(string strIdSession, string strCustomerId, string strStatus, string strContract, string strType, string platform, string flagconvivencia)
        {
            PostpaidService.AnnotationsResponsePostPaid objAnnotationsResponse = null;
            Areas.Dashboard.Models.PostpaidDataAccount.AccountAnnotationModel oPostDataAnnotationModel = null;
            PostpaidService.AnnotationsRequestPostPaid objAnnotationsRequest = new PostpaidService.AnnotationsRequestPostPaid
            {
                CustomerId = strCustomerId,
                Contract = strContract,
                Status = strStatus,
                NumberTickler = "",
                Type = strType,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
                plataforma = platform,
                flagconvivencia = flagconvivencia
            };

            try
            {
                objAnnotationsResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.AnnotationsResponsePostPaid>(() =>
                {
                    return objPostpaidService.GetAnnotationWS(objAnnotationsRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objAnnotationsRequest.audit.transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
              
                Claro.Web.Logging.Error(strIdSession, objAnnotationsRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objAnnotationsRequest.audit.transaction);
            }

            if (objAnnotationsResponse != null)
            {
                oPostDataAnnotationModel = new Models.PostpaidDataAccount.AccountAnnotationModel();
                if (objAnnotationsResponse.ListAnnotation != null)
                {
                    Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountAnnotation.Annotation objAnnotation = null;

                    foreach (PostpaidService.AnnotationPostPaid item in objAnnotationsResponse.ListAnnotation)
                    {
                        objAnnotation = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountAnnotation.Annotation()
                        {
                            Code = item.CODIGO, //tobe si
                            Status = item.ESTADO,//tobe si
                            Priority = item.PRIORIDAD,// tobe no
                            Description = item.DESCRIPCION,// tobe si
                            UserTracing = item.USUARIO_SEGUIMIENTO, //tobe no
                            DateTracing = Convert.ToDateNullable(item.FECHA_SEGUIMIENTO), //tobe SI
                            ActionTracing = item.ACCION_SEGUIMIENTO, // tobe no
                            NumberTickler = item.NRO_TICKLER,// tobe no
                            StatusAction = item.ESTADO_ACTION,//tobe no
                            DateClosing = item.FECHA_CIERRE, //tobe si
                            DateOpening = item.FECHA_APERTURA, // tobe si
                            DateModify = item.FECHA_MODI//tobe si

                        };

                        oPostDataAnnotationModel.listAnnotationPast.Add(objAnnotation);
                    }
                    if (oPostDataAnnotationModel.listAnnotationPast != null && oPostDataAnnotationModel.listAnnotationPast.Count > 0)
                        oPostDataAnnotationModel.listAnnotationPast = oPostDataAnnotationModel.listAnnotationPast.OrderByDescending(x => x.DateTracing).ToList();
                }
            }
            else
            {
                oPostDataAnnotationModel = new Models.PostpaidDataAccount.AccountAnnotationModel();
                oPostDataAnnotationModel.Transaction = objAnnotationsRequest.audit.transaction;
            }

            try
            {
                SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strCustomerId, KEY.AppSettings("strAudiTraCodAnotaciones"), Claro.SIACU.Constants.Annotations); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objAnnotationsRequest.audit.transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));               
                throw new Claro.MessageException(objAnnotationsRequest.audit.transaction);
            }
            string hidden = platform.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase) ? "hidden" : "col-sm-2 sorting";
            ViewBag.hiddenTobe = hidden;
            return PartialView(oPostDataAnnotationModel);
        }

        public ActionResult AccountDetailAnnotation(string strIdSession, string strCustomerId, string strNumberTickler, string strDescription, string strUsuCrea, string strFechaCrea, string strCodeAnotacion,
                                                    string strdateClose, string strdateOpen, string strdateModif, string platform)
        {
            PostpaidService.DetailAnnotationRequestPostPaid objDetailAnnotationRequest = new PostpaidService.DetailAnnotationRequestPostPaid()
            {
                CustomerId = strCustomerId,
                NumberTickler = strNumberTickler,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };
            Areas.Dashboard.Models.PostpaidDataAccount.AccountDetailAnnotationModel oPostDataAnnotationModel = new Models.PostpaidDataAccount.AccountDetailAnnotationModel();
            try
            {
                if (platform.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
                {
                    oPostDataAnnotationModel.Description = strDescription;
                    oPostDataAnnotationModel.Date_Closing = strdateClose;
                    oPostDataAnnotationModel.Date_Modification = strdateModif;
                    oPostDataAnnotationModel.Date_Opening = strdateOpen;

                    ViewBag.hiddenTobe = "hidden";
                }
                else
                {
                    if (strNumberTickler != "")
                    {
                        PostpaidService.DetailAnnotationResponsePostPaid objDetailAnnotationResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.DetailAnnotationResponsePostPaid>(() => { return objPostpaidService.GetDetailAnnotation(objDetailAnnotationRequest); });
                        if (objDetailAnnotationResponse.DetailAnnotation != null)
                        {

                            oPostDataAnnotationModel.Description = objDetailAnnotationResponse.DetailAnnotation.DESCRIPCION;
                            oPostDataAnnotationModel.Date_Closing = objDetailAnnotationResponse.DetailAnnotation.FECHA_CIERRE;
                            oPostDataAnnotationModel.Date_Modification = objDetailAnnotationResponse.DetailAnnotation.FECHA_MODIFICACION;
                            oPostDataAnnotationModel.Date_Opening = objDetailAnnotationResponse.DetailAnnotation.FECHA_APERTURA;
                            oPostDataAnnotationModel.Date_Tracing = objDetailAnnotationResponse.DetailAnnotation.FECHA_SEGUIMIENTO;
                            oPostDataAnnotationModel.User_Closing = objDetailAnnotationResponse.DetailAnnotation.USUARIO_CIERRE;
                            oPostDataAnnotationModel.User_Modification = objDetailAnnotationResponse.DetailAnnotation.USUARIO_MODIFICACION;
                            oPostDataAnnotationModel.User_Opening = objDetailAnnotationResponse.DetailAnnotation.USUARIO_APERTURA;

                        }
                    }
                    else
                    {
                        oPostDataAnnotationModel.Description = strDescription;
                        oPostDataAnnotationModel.Date_Closing = "";
                        oPostDataAnnotationModel.Date_Modification = "";
                        oPostDataAnnotationModel.Date_Opening = strFechaCrea;
                        oPostDataAnnotationModel.Date_Tracing = "";
                        oPostDataAnnotationModel.User_Closing = "";
                        oPostDataAnnotationModel.User_Modification = "";
                        oPostDataAnnotationModel.User_Opening = strUsuCrea;
                    }
                    ViewBag.hiddenTobe = "";
                    List<string> CodigoAnotacionBloqueoEquipo = null;
                    CodigoAnotacionBloqueoEquipo = KEY.AppSettings("StrCodigoAnotacionBloqueoEquipo").Split('|').ToList();

                    if (strCodeAnotacion != "" && strCodeAnotacion != null && CodigoAnotacionBloqueoEquipo != null)
                    {
                        foreach (var item in CodigoAnotacionBloqueoEquipo)
                        {
                            if (strCodeAnotacion.ToUpper().Trim().Equals(item.ToString().ToUpper().Trim()))
                            {
                                oPostDataAnnotationModel.Description = KEY.AppSettings("StrDescripcionAnotacionBloqueoEquipo");
                            }
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDetailAnnotationRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objDetailAnnotationRequest.audit.transaction);
            }


            return PartialView(oPostDataAnnotationModel);
        }

        public ActionResult AccountWarranty(string strIdSession, string strCustomerId, string strDocumentNumber, string strDocumentType, string strAplication)
        {
            PostpaidService.WarrantyRequestPostPaid objGetWarrantyRequest = new PostpaidService.WarrantyRequestPostPaid()
            {
                CodApplication = App_Code.Common.GetApplicationCode(),
                UserApplication = App_Code.Common.CurrentUser,
                CustomerId = strCustomerId,
                DocumentNumber = strDocumentNumber,
                DocumentType = strDocumentType,
                Aplication = strAplication,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };
            Areas.Dashboard.Models.PostpaidDataAccount.AccountWarrantyModel oPostDataWarrantyModel = new Areas.Dashboard.Models.PostpaidDataAccount.AccountWarrantyModel();

            try
            {
                PostpaidService.WarrantyResponsePostPaid objGetWarrantyResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.WarrantyResponsePostPaid>(objGetWarrantyRequest.audit.Session, objGetWarrantyRequest.audit.transaction, () => { return objPostpaidService.GetWarranty(objGetWarrantyRequest); });

                if (objGetWarrantyResponse != null && objGetWarrantyResponse.ObjWarranty != null)
                {
                    if (objGetWarrantyResponse.ObjWarranty.lstCabAccountWarranty != null)
                    {
                        List<HELPER.AccountWarranty.AccountWarranty> lstCabAccountWarranty = new List<HELPER.AccountWarranty.AccountWarranty>();
                        foreach (PostpaidService.WarrantyPostPaid objWarranty in objGetWarrantyResponse.ObjWarranty.lstCabAccountWarranty)
                        {
                            lstCabAccountWarranty.Add(new HELPER.AccountWarranty.AccountWarranty()
                            {
                                Number = objWarranty.NUMERO,
                                Date = objWarranty.FECHA,
                                State = objWarranty.ESTADO,
                                Amount = objWarranty.MONTO,
                                Residue = objWarranty.SALDO
                            });
                        }
                        oPostDataWarrantyModel.lstCabAccountWarranty = lstCabAccountWarranty;
                    }

                    if (objGetWarrantyResponse.ObjWarranty.lstDebAccountWarranty != null)
                    {
                        List<HELPER.AccountWarranty.AccountWarranty> lstDebAccountWarranty = new List<HELPER.AccountWarranty.AccountWarranty>();
                        foreach (PostpaidService.WarrantyPostPaid objWarranty in objGetWarrantyResponse.ObjWarranty.lstDebAccountWarranty)
                        {
                            lstDebAccountWarranty.Add(new HELPER.AccountWarranty.AccountWarranty()
                            {
                                Number = objWarranty.NUMERO,
                                Date = objWarranty.FECHA,
                                State = objWarranty.ESTADO,
                                Amount = objWarranty.MONTO,
                                Residue = objWarranty.SALDO
                            });
                        }
                        oPostDataWarrantyModel.lstDebAccountWarranty = lstDebAccountWarranty;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetWarrantyRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetWarrantyRequest.audit.transaction);
            }

            return PartialView(oPostDataWarrantyModel);
        }

        public ActionResult AccountSharedBag(string strIdSession, string srtAccount, string srtValueSearch, string strCustomerId, string strCustomerType, string strtypeSearch)
        {
            PostpaidService.SharedBagResponsePostPaid objGetSharedBagResponse;

            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBag> lstSharedBag = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBag>();
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBagLine> lstSharedBagLine = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBagLine>();

            PostpaidService.SharedBagRequestPostPaid objGetSharedBagRequest = new PostpaidService.SharedBagRequestPostPaid()
            {
                Customerid = strCustomerId,
                Account = srtAccount,
                Telephone = string.Equals(strtypeSearch, Claro.Constants.NumberOneString) ? srtValueSearch : "",
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
            };

            Areas.Dashboard.Models.PostpaidDataAccount.AccountSharedBagModel oPostDataSharedBagModel = new Areas.Dashboard.Models.PostpaidDataAccount.AccountSharedBagModel()
            {
                Account = srtAccount,
                Line = srtValueSearch,
                lstSharedBag = null,
                lstSharedBagLine = null
            };
            try
            {
                objGetSharedBagResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.SharedBagResponsePostPaid>(
                    () => { return objPostpaidService.GetSharedBag(objGetSharedBagRequest); });

                if (objGetSharedBagResponse.ListSharedBag != null)
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
                        });
                    }

                    if (objGetSharedBagResponse.ListSharedBag.Count > 0)
                    {
                        lstSharedBag = new List<HELPER.AccountSharedBag.AccountSharedBag>();
                        foreach (PostpaidService.SharedBagPostPaid item in objGetSharedBagResponse.ListSharedBag)
                        {
                            lstSharedBagLine.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountSharedBag.AccountSharedBagLine()
                            {
                                Bag = item.BOLSA,
                                Line = item.LINEA,
                                Unit = item.UNIDAD,
                                Stopper = item.TOPE,
                                Consumption = item.CONSUMO,
                                Balance = item.SALDO,
                                DateValidity = item.FECHAVIGENCIA
                            });
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                lstSharedBag = null;
                lstSharedBagLine = null;
                oPostDataSharedBagModel.Error = ex.InnerException.Message;
                Claro.Web.Logging.Error(strIdSession, objGetSharedBagRequest.audit.transaction, ex.Message);
            }

            try
            {
                SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
                Claro.Web.Logging.ExecuteMethod<string>(
                    () => { return App_Code.Common.InsertAudit(objaudit, srtAccount, KEY.AppSettings("strAudiTraCodBolsaCompartida"), Claro.SIACU.Constants.AccountSharedBag); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetSharedBagRequest.audit.transaction, ex.Message);
            }


            oPostDataSharedBagModel.isData = true;
            oPostDataSharedBagModel.lstSharedBag = lstSharedBag;
            oPostDataSharedBagModel.lstSharedBagLine = lstSharedBagLine;

            if (lstSharedBag.Count == 0 && lstSharedBagLine.Count == 0) oPostDataSharedBagModel.isData = false;
            oPostDataSharedBagModel.Error = KEY.AppSettings("strMsgErrorTransaccion");
            return PartialView(oPostDataSharedBagModel);

        }

        public PINPUK_MODEL GetPinPuk(string strIdSession, string srtAccount, string strBusinessName)
        {
            PINPUK_MODEL objPinPukModel = new PINPUK_MODEL()
            {
                Account = srtAccount,
                BusinessName = strBusinessName,
                Date = DateTime.Now.ToShortDateString()
            };

            PostpaidService.PinPukRequestPostPaid objGetPinPukRequest = new PostpaidService.PinPukRequestPostPaid()
            {
                Account = srtAccount,
                Type = Claro.Constants.LetterC,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.PinPukResponsePostPaid objGetPinPukResponse = null;

            try
            {
                objGetPinPukResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.PinPukResponsePostPaid>(
                    () => { return objPostpaidService.GetPinPuk(objGetPinPukRequest); });

                if (objGetPinPukResponse.ListPinPuk != null && objGetPinPukResponse.ListPinPuk.Count > 0)
                {
                    int intNumero = Claro.Constants.NumberZero;
                    foreach (PostpaidService.PinyPukPostPaid item in objGetPinPukResponse.ListPinPuk)
                    {
                        intNumero++;
                        objPinPukModel.lstAccountPinPuk.Add(new HELPER.AccountPinPukHelper.AccountPinPuk()
                        {
                            Nro = intNumero,
                            ICC_IC = item.ICC_IC,
                            IMSI = item.IMSI,
                            MSISDN = item.MSISDN,
                            PIN1 = item.PIN1,
                            PUK1 = item.PUK1
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                objPinPukModel = null;
                Claro.Web.Logging.Error(strIdSession, objGetPinPukRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetPinPukRequest.audit.transaction);
            }

            return objPinPukModel;
        }

        public ActionResult AccountPinPuk(string strIdSession, string srtAccount, string strBusinessName)
        {
            return PartialView(GetPinPuk(strIdSession, srtAccount, strBusinessName));
        }

        public JsonResult AccountExportPinPuk(string strIdSession, string srtAccount, string strBusinessName)
        {
            PINPUK_MODEL objPinPukModel = GetPinPuk(strIdSession, srtAccount, strBusinessName);
            string path = oExcelHelper.ExportExcel(objPinPukModel, TemplateExcel.CONST_PIN_PUK);
            return Json(path);
        }
        private int FlagBagService = 0;
        public RELATIONPLAN_MODEL GetRelationPlans(string strIdSession, Areas.Dashboard.Models.Postpaid.DataCustomerModel objCustomer)
        {
            RELATIONPLAN_MODEL objRelationPlanModel = null;
            PostpaidService.RelationPlansResponsePostPaid objGetRelationPlansResponse = null;
            PostpaidService.MassiveRequestPostPaid objMassiveRequestPostPaid;

            if (objCustomer != null)
            {
                objRelationPlanModel = new RELATIONPLAN_MODEL();
                objRelationPlanModel.objCustomer.Account = objCustomer.Account;
                objRelationPlanModel.objCustomer.BusinessName = objCustomer.BusinessName;
                objRelationPlanModel.objCustomer.CreditLimit = objCustomer.objPostDataAccount.CreditLimit;
                objRelationPlanModel.objCustomer.CustomerContact = objCustomer.CustomerContact;
                objRelationPlanModel.objCustomer.CustomerID = objCustomer.CustomerID;
                objRelationPlanModel.objCustomer.DNIRUC = objCustomer.DNIRUC;
                objRelationPlanModel.objCustomer.Email = objCustomer.Email;
                objRelationPlanModel.objCustomer.Fax = objCustomer.Fax;
                objRelationPlanModel.objCustomer.FullName = objCustomer.FullName;
                objRelationPlanModel.objCustomer.InvoiceAddress = objCustomer.InvoiceAddress;
                objRelationPlanModel.objCustomer.InvoiceDistrict = objCustomer.InvoiceDistrict;
                objRelationPlanModel.objCustomer.InvoiceProvince = objCustomer.InvoiceProvince;
                objRelationPlanModel.objCustomer.LegalAgent = objCustomer.LegalAgent;
                objRelationPlanModel.objCustomer.PhoneReference = objCustomer.PhoneReference;
                objRelationPlanModel.objCustomer.TotalLines = objCustomer.objPostDataAccount.TotalLines;
                objRelationPlanModel.objCustomer.Consultant = objCustomer.objPostDataAccount.Consultant_Account;

                PostpaidService.HeaderRequest objHeaderRequest = new PostpaidService.HeaderRequest()
                {
                    country = KEY.AppSettings("country"),
                    language = KEY.AppSettings("country"),
                    consumer = KEY.AppSettings("consumer"),
                    system = KEY.AppSettings("system"),
                    modulo = KEY.AppSettings("modulo"),
                    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    userId = App_Code.Common.CurrentUser,
                    dispositivo = App_Code.Common.GetClientIP(),
                    wsIp = App_Code.Common.GetApplicationIpServer(),
                    operation = KEY.AppSettings("getRelationPlans"),
                    timestamp = DateTime.Now.ToString(),
                    msgType = KEY.AppSettings("msgType"),
                    VarArg = KEY.AppSettings("VarArg")
                };

                objMassiveRequestPostPaid = new PostpaidService.MassiveRequestPostPaid();
                objMassiveRequestPostPaid.MessageRequest = new PostpaidService.MassiveMessageRequest();
                objMassiveRequestPostPaid.MessageRequest.Header = new PostpaidService.HeadersRequest();
                objMassiveRequestPostPaid.MessageRequest.Body = new PostpaidService.MassiveBodyRequest();
                objMassiveRequestPostPaid.MessageRequest.Header.HeaderRequest = objHeaderRequest;
                objMassiveRequestPostPaid.MessageRequest.Body.customerId = objCustomer.CustomerID;
                objMassiveRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objMassiveRequestPostPaid.Account = objCustomer.Account;
                objMassiveRequestPostPaid.CustomerType = objCustomer.CustomerType;

                try
                {
                    objGetRelationPlansResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.RelationPlansResponsePostPaid>(
                        () => { return objPostpaidService.GetRelationPlans(objMassiveRequestPostPaid); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objMassiveRequestPostPaid.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objMassiveRequestPostPaid.audit.transaction);
                }

                objRelationPlanModel.objAccountBag.FlagBagService = Claro.Constants.NumberZero;
                if (objGetRelationPlansResponse.ObjBag != null && int.Equals(objGetRelationPlansResponse.ObjBag.FLAG_DESACTIVA, 1))
                {
                    objRelationPlanModel.objAccountBag.Account = objGetRelationPlansResponse.ObjBag.CUENTA;
                    objRelationPlanModel.objAccountBag.ActivationDate = objGetRelationPlansResponse.ObjBag.FECHA_ACTIVACION;
                    objRelationPlanModel.objAccountBag.BagType = objGetRelationPlansResponse.ObjBag.TIPO_BOLSA;
                    objRelationPlanModel.objAccountBag.FixedCharge = objGetRelationPlansResponse.ObjBag.CARGO_FIJO;
                    objRelationPlanModel.objAccountBag.MinSol = objGetRelationPlansResponse.ObjBag.MIN_SOL;
                    objRelationPlanModel.objAccountBag.NumberLines = objGetRelationPlansResponse.ObjBag.CANTIDAD_LINEAS;
                }

                if (objGetRelationPlansResponse.ListBagDetail != null)
                {
                    HELPER.AccountRelationPlanHelper.AccountBagDetail objBagDet;

                    foreach (PostpaidService.BagDetailPostPaid item in objGetRelationPlansResponse.ListBagDetail)
                    {
                        objBagDet = new HELPER.AccountRelationPlanHelper.AccountBagDetail();
                        objBagDet.ActivationDateBagDetail = item.FECHA_ACTIVACION;
                        objBagDet.BagTypeBagDetail = item.TIPO_BOLSA;
                        objBagDet.FixedChargeBagDetail = item.CARGO_FIJO;
                        objBagDet.MinSolBagDetail = item.MIN_SOL;
                        objBagDet.NumberLinesBagDetail = item.CANTIDAD_LINEAS;
                        objRelationPlanModel.lstAccountBagDetail.Add(objBagDet);
                    }
                    objRelationPlanModel.objAccountBag.FlagBagService = Claro.Constants.NumberOne;
                    FlagBagService = Claro.Constants.NumberOne;
                }

                if (objGetRelationPlansResponse.ListPlan != null)
                {
                    int intDina = Claro.Constants.NumberZero;
                    foreach (PostpaidService.TabPlanesMassivePostType item in objGetRelationPlansResponse.ListPlan.MessageResponse.Body.responseDataObtenerTabPlanesMasivoPost.listaTabPlanesMasivoPost.tabPlanesMasivoPost)
                    {
                        intDina++;
                        int intDynamicRows = default(int);

                        if (int.Equals(intDina, Claro.Constants.NumberOne))
                        {
                            intDynamicRows =
                                        Convert.ToInt(item.fLdi) +
                                        Convert.ToInt(item.fRoamInt) +
                                        Convert.ToInt(item.fTimData) +
                                        Convert.ToInt(item.fTimFax) +
                                        Convert.ToInt(item.fPaqueteSms) +
                                        Convert.ToInt(item.fPaqueteData) +
                                        Convert.ToInt(item.fNivDeHab) +
                                        Convert.ToInt(item.fRpv) +
                                        Convert.ToInt(item.fCostoRpv) +
                                        Convert.ToInt(item.fCostoRpvNac) +
                                        Convert.ToInt(item.fSeguro) +
                                        Convert.ToInt(item.fPrestamo) +
                                        Convert.ToInt(item.fTimProConnexion) +
                                        Convert.ToInt(item.fGprs) +
                                        Convert.ToInt(item.fLbs) +
                                        Convert.ToInt(item.fSolsms) +
                                        Convert.ToInt(item.fRtp) +
                                        Convert.ToInt(item.fHabilitacion) +
                                        Convert.ToInt(item.fSmsMail) +
                                        Convert.ToInt(item.fRpceIlimitado) +
                                        Convert.ToInt(item.fCtaPersRecarga) +
                                        Convert.ToInt(item.fSms) +
                                        Convert.ToInt(item.fMms) +
                                        Convert.ToInt(item.fBlackberry) +
                                        Convert.ToInt(item.fReposicion) +
                                        Convert.ToInt(item.fControlConsumoAdic) +
                                        Convert.ToInt(item.fControlConsumoAdic) +
                                        Convert.ToInt(item.fFactDetallada) +
                                        Convert.ToInt(item.fFactDetModA) +
                                        Convert.ToInt(item.fClaroDirecto) +
                                        Convert.ToInt(item.fCargoFactDet) +
                                        Convert.ToInt(item.fClaroFax) +
                                        Convert.ToInt(item.fClaroData) +
                                        Convert.ToInt(item.fClarobanca);

                        }

                        objRelationPlanModel.lstAccountRelationPlan.Add(new HELPER.AccountRelationPlanHelper.AccountRelationPlan()
                        {
                            IT = intDina,
                            Telephone = item.telefono,
                            SIMCard = item.simCard,
                            NichoID = item.nichoId,
                            DescriptionPlan = item.plan,
                            FixedCharge = item.acceso,
                            ActivationDate = item.fechaActiv,
                            F_LDI = item.fLdi,
                            LDI = item.ldi,
                            F_InternationalRoaming = item.fRoamInt,
                            InternationalRoaming = item.roamInt,
                            F_TimData = item.fTimData,
                            TimData = item.timData,
                            F_TimFax = item.fTimFax,
                            TimFax = item.timFax,
                            F_SMSPackage = item.fPaqueteSms,
                            SMSPackage = item.paqueteSms,
                            F_DataPackage = item.fPaqueteData,
                            DataPackage = item.paqueteData,
                            F_LevelEnablement = item.fNivDeHab,
                            HabilitacionLevel = item.nivDeHab,
                            F_RPV = item.fRpv,
                            PreferentialRateRPC = item.rpv,
                            F_CostRPV = item.fCostoRpv,
                            CostXRPCe = item.costoRpv,
                            F_CostRPCUnlimited = item.fCostoRpvNac,
                            CostRPCUnlimited = item.costoRpvNac,
                            F_Insurance = item.fSeguro,
                            Insurance = item.seguro,
                            F_Loan = item.fPrestamo,
                            Loan = item.prestamo,
                            F_TimProConnection = item.fTimProConnexion,
                            TimProConnection = item.timProConnexion,
                            F_GPRS = item.fGprs,
                            GPRS = item.gprs,
                            F_LBS = item.fLbs,
                            LBS = item.lbs,
                            F_SolSMS = item.fSolsms,
                            SolSMS = item.solsms,
                            F_RTP = item.fRtp,
                            RTP = item.rtp,
                            F_Habilitation = item.fHabilitacion,
                            Habilitation = item.habilitacion,
                            F_SMSMail = item.fSmsMail,
                            SMSMail = item.smsMail,
                            F_RPCUnlimited = item.fRpceIlimitado,
                            RPCUnlimited = item.rpceIlimitado,
                            F_RechargePersonalAccount = item.fCtaPersRecarga,
                            RechargePersonalAccount = item.ctaPersRecarga,
                            F_SMS = item.fSms,
                            SMSSending = item.sms,
                            F_MMS = item.fMms,
                            GPRSMMS = item.mms,
                            F_Blackberry = item.fBlackberry,
                            BlackBerry = item.blackberry,
                            F_Reposicion = item.fReposicion,
                            Reposicion = item.reposicion,
                            F_AdditionalConsumptionControl = item.fControlConsumoAdic,
                            AdditionalTopBag = item.topeSolesAdic,
                            ExactlyTopBag = item.topeBolsaExacto,
                            F_DetailedBilling = item.fFactDetallada,
                            DetailedBilling = item.factDetallada,
                            F_DetailedBillingModA = item.fFactDetModA,
                            DetailedBillingModA = item.factDetModA,
                            F_ClaroDirect = item.fClaroDirecto,
                            ClaroDirect = item.claroDirecto,
                            F_DetailedBillingCharge = item.fCargoFactDet,
                            DetailedBillingCharge = item.cargoFactDet,
                            F_ClaroFax = item.fClaroFax,
                            ClaroFax = item.claroFax,
                            F_ClaroData = item.fClaroData,
                            ClaroData = item.claroData,
                            F_Clarobanking = item.fClarobanca,
                            Clarobanking = item.clarobanca,
                            WithNoEquipment = item.campana,
                            BusinessConsultant = item.consultor,
                            WFCode = item.codigoWf,
                            State = item.estado,
                            Reason = item.motivo,
                            ConsultanRenovating = item.consultorRenovacion,
                            DeliveryDateRenewalTeam = item.fechaEquipoRenov,
                            RenovatingActivationDate = item.fechaActivRenov,
                            DynamicRows = intDynamicRows,
                            BagService = item.bolsaServicios
                        });
                    }
                }
            }
            return objRelationPlanModel;
        }

        public JsonResult GetSolicitude(string strIdSession, string strCustomerId)
        {
            PostpaidService.SolicitudeResponse objSolicitudeResponse = null;
            PostpaidService.SolicitudeRequests objSolicitudeRequests;

            PostpaidService.HeaderRequest objHeaderRequest = new PostpaidService.HeaderRequest()
                {
                    country = KEY.AppSettings("country"),
                    language = KEY.AppSettings("language"),
                    consumer = KEY.AppSettings("consumer"),
                    system = KEY.AppSettings("system"),
                    modulo = KEY.AppSettings("modulo"),
                    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    userId = App_Code.Common.CurrentUser,
                    dispositivo = App_Code.Common.GetClientIP(),
                    wsIp = App_Code.Common.GetApplicationIpServer(),
                    operation = KEY.AppSettings("getSolicitude"),
                    timestamp = DateTime.Now.ToString(),
                    msgType = KEY.AppSettings("msgType"),
                    VarArg = KEY.AppSettings("VarArg")
                };

            objSolicitudeRequests = new PostpaidService.SolicitudeRequests();
            objSolicitudeRequests.MessageRequest = new PostpaidService.SolicitudeMessageRequest();
            objSolicitudeRequests.MessageRequest.Header = new PostpaidService.HeadersRequest();
            objSolicitudeRequests.MessageRequest.Body = new PostpaidService.SolicitudeBodyRequest();
            objSolicitudeRequests.MessageRequest.Header.HeaderRequest = objHeaderRequest;
            objSolicitudeRequests.MessageRequest.Body.customerid = strCustomerId;
            objSolicitudeRequests.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

            try
            {
                objSolicitudeResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.SolicitudeResponse>(
                    () => { return objPostpaidService.GetSolicitude(objSolicitudeRequests); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objSolicitudeRequests.audit.transaction, ex.Message);
                throw new Claro.MessageException(objSolicitudeRequests.audit.transaction);
            }

            return Json(new { data = objSolicitudeResponse.MessageResponse.Body.responseDataObtenerSolicitudes.listaObtenerSolicitudes.obtenerSolicitudes });
        }

        public JsonResult RegisterSolicitude(string strIdSession, string strCustomerId, Areas.Dashboard.Models.Postpaid.DataCustomerModel objCustomer, string strApplication)
        {
            PostpaidService.RegisterSolicitudeRequests objRegisterSolicitudeRequests;
            PostpaidService.RegisterSolicitudeResponse objRegisterSolicitudeResponse = null;

            PostpaidService.HeaderRequest objHeaderRequest = new PostpaidService.HeaderRequest()
            {
                country = KEY.AppSettings("country"),
                language = KEY.AppSettings("country"),
                consumer = KEY.AppSettings("consumer"),
                system = KEY.AppSettings("system"),
                modulo = KEY.AppSettings("modulo"),
                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                userId = App_Code.Common.CurrentUser,
                dispositivo = App_Code.Common.GetClientIP(),
                wsIp = App_Code.Common.GetApplicationIpServer(),
                operation = KEY.AppSettings("registerSolicitude"),
                timestamp = DateTime.Now.ToString(),
                msgType = KEY.AppSettings("msgType"),
                VarArg = KEY.AppSettings("VarArg")
            };

            objRegisterSolicitudeRequests = new PostpaidService.RegisterSolicitudeRequests();
            objRegisterSolicitudeRequests.MessageRequest = new PostpaidService.RegisterSolicitudeMessageRequest();
            objRegisterSolicitudeRequests.MessageRequest.Header = new PostpaidService.HeadersRequest();
            objRegisterSolicitudeRequests.MessageRequest.Header.HeaderRequest = objHeaderRequest;
            objRegisterSolicitudeRequests.MessageRequest.Body = new PostpaidService.RegisterSolicitudeBodyRequest();
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitudPendiente = new PostpaidService.RegisterSolicitudePendingRequest();
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitudPendiente.customerid = strCustomerId;
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud = new PostpaidService.RegisterSolicitudeRequest();
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.usuario = App_Code.Common.CurrentUser;
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.customerid = strCustomerId;
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.aplicativo = strApplication;
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.lineasActivas = Claro.Constants.NumberOneString;

            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte = new PostpaidService.HeaderReport();
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente = new PostpaidService.CustomerData();
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.razonSocial = objCustomer.BusinessName.ToString().Replace(((char)34).ToString(), ((char)92).ToString() + ((char)34).ToString()) ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.direFacturacion = objCustomer.InvoiceAddress ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.contactoFacturas = objCustomer.FullName ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.contactoCliente = objCustomer.CustomerContact ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.limiteCredito = objCustomer.objPostDataAccount.CreditLimit ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.accountManager = objCustomer.objPostDataAccount.Consultant_Account ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.nroAcuerdoPCS = string.Empty;
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.distrito = objCustomer.InvoiceDistrict ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.repLegal = objCustomer.LegalAgent ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.tlfReferencia = objCustomer.PhoneReference ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.email = objCustomer.Email ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.fechaActivacion = string.Empty;
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.codCliente = objCustomer.Account ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.provincia = objCustomer.InvoiceProvince ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.ruc = objCustomer.DNIRUC ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.fax = objCustomer.Fax ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.totalLineasA = objCustomer.objPostDataAccount.TotalLines ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.datosCliente.envioReciboCorreo = string.Empty;

            RELATIONPLAN_MODEL objRelationPlan = GetRelationPlans(strIdSession, objCustomer);

            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.bolsaPrincipal = new PostpaidService.BagMain();
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.bolsaPrincipal.tipoBolsa = objRelationPlan.objAccountBag.BagType ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.bolsaPrincipal.cargoFijo = objRelationPlan.objAccountBag.FixedCharge ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.bolsaPrincipal.fechaActivacion = objRelationPlan.objAccountBag.ActivationDate ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.bolsaPrincipal.minSol = objRelationPlan.objAccountBag.MinSol ?? "";
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.bolsaPrincipal.cantLineas = objRelationPlan.objAccountBag.NumberLines ?? "";

            List<PostpaidService.BagDetail> listBagDetail = new List<PostpaidService.BagDetail>();

            foreach (HELPER.AccountRelationPlanHelper.AccountBagDetail item in objRelationPlan.lstAccountBagDetail)
            {
                listBagDetail.Add(new PostpaidService.BagDetail()
                    {
                        bolsa = item.BagTypeBagDetail ?? "",
                        cargo = item.FixedChargeBagDetail ?? "",
                        fechaActivacion = item.ActivationDateBagDetail ?? "",
                        unidades = item.MinSolBagDetail ?? "",
                        cantidadLineas = item.NumberLinesBagDetail ?? ""
                    });
            }

            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.listaBolsaDetalle = new PostpaidService.ListBagDetail();
            objRegisterSolicitudeRequests.MessageRequest.Body.registrarSolicitud.cabeceraReporte.listaBolsaDetalle.bolsaDetalle = listBagDetail;

            objRegisterSolicitudeRequests.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

            try
            {
                objRegisterSolicitudeResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.RegisterSolicitudeResponse>(
                    () => { return objPostpaidService.RegisterSolicitude(objRegisterSolicitudeRequests); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRegisterSolicitudeRequests.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRegisterSolicitudeRequests.audit.transaction);
            }

            return Json(new { data = objRegisterSolicitudeResponse });
        }

        public JsonResult GetExcelRelationPlan(string strIdSession, string strFileName)
        {
            PostpaidService.DocumentSolicitudeResponse objDocumentSolicitudeResponse = null;
            PostpaidService.DocumentSolicitudeRequests objDocumentSolicitudeRequests;

            PostpaidService.HeaderRequest objHeaderRequest = new PostpaidService.HeaderRequest()
            {
                country = KEY.AppSettings("country"),
                language = KEY.AppSettings("language"),
                consumer = KEY.AppSettings("consumer"),
                system = KEY.AppSettings("system"),
                modulo = KEY.AppSettings("modulo"),
                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                userId = App_Code.Common.CurrentUser,
                dispositivo = App_Code.Common.GetClientIP(),
                wsIp = App_Code.Common.GetApplicationIpServer(),
                operation = KEY.AppSettings("getDocumentSolicitude"),
                timestamp = DateTime.Now.ToString(),
                msgType = KEY.AppSettings("msgType"),
                VarArg = KEY.AppSettings("VarArg")
            };

            objDocumentSolicitudeRequests = new PostpaidService.DocumentSolicitudeRequests();
            objDocumentSolicitudeRequests.MessageRequest = new PostpaidService.DocumentSolicitudeMessageRequest();
            objDocumentSolicitudeRequests.MessageRequest.Header = new PostpaidService.HeadersRequest();
            objDocumentSolicitudeRequests.MessageRequest.Header.HeaderRequest = objHeaderRequest;
            objDocumentSolicitudeRequests.MessageRequest.Body = new PostpaidService.DocumentSolicitudeBodyRequest();
            objDocumentSolicitudeRequests.MessageRequest.Body.usuario = KEY.AppSettings("userDownload");
            objDocumentSolicitudeRequests.MessageRequest.Body.clave = KEY.AppSettings("keyDownload");
            objDocumentSolicitudeRequests.MessageRequest.Body.ruta = KEY.AppSettings("routeDownload");
            objDocumentSolicitudeRequests.MessageRequest.Body.codigoAplicacion = KEY.AppSettings("codeApplicationDownload");
            objDocumentSolicitudeRequests.MessageRequest.Body.puerto = KEY.AppSettings("portDownload");
            objDocumentSolicitudeRequests.MessageRequest.Body.ip = KEY.AppSettings("ipDownload");
            objDocumentSolicitudeRequests.MessageRequest.Body.nombreDocumento = strFileName;
            objDocumentSolicitudeRequests.MessageRequest.Body.extension = KEY.AppSettings("extensionDownload");
            objDocumentSolicitudeRequests.MessageRequest.Body.flag = string.Empty;
            objDocumentSolicitudeRequests.MessageRequest.Body.cantidad = string.Empty;

            objDocumentSolicitudeRequests.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

            string path = string.Empty;

            try
            {
                objDocumentSolicitudeResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.DocumentSolicitudeResponse>(
                    () => { return objPostpaidService.GetDocumentSolicitude(objDocumentSolicitudeRequests); });

                switch (objDocumentSolicitudeResponse.MessageResponse.Body.codigoRespuesta)
                {
                    case null:
                        return Json("error");
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "-1":
                    case "-2":
                    case "-3":
                    case "-4":
                        return Json(objDocumentSolicitudeResponse.MessageResponse.Body.codigoRespuesta);
                }

                path = Path.GetTempFileName();

                FileStream FileStreamObject = new FileStream(path, FileMode.Create, FileAccess.Write);
                FileStreamObject.Write(objDocumentSolicitudeResponse.MessageResponse.Body.reporte, 0, objDocumentSolicitudeResponse.MessageResponse.Body.reporte.Length);
                FileStreamObject.Close();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDocumentSolicitudeRequests.audit.transaction, ex.Message);
                throw new Claro.MessageException(objDocumentSolicitudeRequests.audit.transaction);
            }

            var json = Json(path);
            return json;
        }

        public ActionResult AccountCreditLimit(string strIdSession, string srtAccount, string strBusinessName, string strCustomerId, string strAplication)
        {
            Areas.Dashboard.Models.PostpaidDataAccount.AccountCreditLimitModel oPostCreditLimitModel = new Areas.Dashboard.Models.PostpaidDataAccount.AccountCreditLimitModel()
            {
                Account = srtAccount,
                BusinessName = strBusinessName
            };

            PostpaidService.CreditLimitResponsePostPaid objGetCreditLimitResponse = null;
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountCreditLimit.AccountCreditLimit> lstAccountCreditLimit = null;

            PostpaidService.CreditLimitRequestPostPaid objGetCreditLimitRequest = new PostpaidService.CreditLimitRequestPostPaid()
            {
                User = App_Code.Common.CurrentUser,
                CustomerId = strCustomerId,
                TypeConsultationDetailOAC = "",
                Application = strAplication,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            try
            {
                objGetCreditLimitResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.CreditLimitResponsePostPaid>(
                    () => { return objPostpaidService.GetCreditLimit(objGetCreditLimitRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetCreditLimitRequest.audit.Session, objGetCreditLimitRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetCreditLimitRequest.audit.transaction);
            }

            if (objGetCreditLimitResponse != null && objGetCreditLimitResponse.ListCreditLimit != null)
            {
                lstAccountCreditLimit = new List<HELPER.AccountCreditLimit.AccountCreditLimit>();
                foreach (PostpaidService.CreditLimitPostPaid item in objGetCreditLimitResponse.ListCreditLimit)
                {
                    lstAccountCreditLimit.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountCreditLimit.AccountCreditLimit()
                    {
                        Fecha_Marca_SMS1 = item.FECHA_MARCA_SMS1,
                        Fecha_Marca_SMS2 = item.FECHA_MARCA_SMS2,
                        Tiene_Pac = item.TIENE_PAC,
                        Monto_Pac = item.MONTO_PAC,
                        Numero_Pac = item.NUMERO_PAC,
                        Fecha_Bloq_Programada = item.FECHA_BLOQ_PROGRAMADA,
                        Fecha_Bloq_Ejecucion = item.FECHA_BLOQ_EJECUCION,
                        Monto_Consumido = item.MONTO_CONSUMIDO,
                        Cargo_Fijo = item.CARGO_FIJO,
                        Cargo_Adicional = item.CARGO_ADICIONAL,
                        Cargo_Prorrat = item.CARGO_PRORRATEO,
                        Fecha_Desbloq_Ejecucion = item.FECHA_DESBLOQ_EJECUCION,
                    });
                }
                oPostCreditLimitModel.lstAccountCreditLimit = lstAccountCreditLimit;
            }

            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
            try
            {

                Claro.Web.Logging.ExecuteMethod<string>(objaudit.Session, objaudit.transaction,
                    () => { return App_Code.Common.InsertAudit(objaudit, srtAccount, KEY.AppSettings("strAudiTraCodBuscaPAC"), KEY.AppSettings("strMensajeBuscarPAC").ToString()); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objaudit.Session, objaudit.transaction, ex.Message);
            }

            return PartialView(oPostCreditLimitModel);
        }

        public ActionResult AccountDetailCreditLimit(string strAccount, string strIdSession, string strDocumentNumber, string strDocumentType, string strNroPac)
        {
            Areas.Dashboard.Models.PostpaidDataAccount.AccountDetailCreditLimitModel oPostCreditLimitDetailModel = null;
            PostpaidService.CreditLimitDetailResponsePostPaid objGetCreditLimitDetailResponse = null;
            if (!string.IsNullOrEmpty(strNroPac))
            {
                PostpaidService.CreditLimitDetailRequestPostPaid objCreditLimitDetailRequest = new PostpaidService.CreditLimitDetailRequestPostPaid()
                {
                    audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
                    CodApplication = strIdSession,
                    UserApplication = App_Code.Common.GetApplicationIp(),
                    DocumentNumber = strDocumentNumber,
                    DocumentType = strDocumentType,
                    Account = strAccount,
                    NumberPac = strNroPac
                };
                try
                {
                    objGetCreditLimitDetailResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.CreditLimitDetailResponsePostPaid>(
                        () => { return objPostpaidService.GetCreditLimitDetail(objCreditLimitDetailRequest); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objCreditLimitDetailRequest.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objCreditLimitDetailRequest.audit.transaction);
                }

                if (objGetCreditLimitDetailResponse.CreditLimitDetail != null)
                {
                    oPostCreditLimitDetailModel = new Areas.Dashboard.Models.PostpaidDataAccount.AccountDetailCreditLimitModel()
                    {
                        AmountPac = objGetCreditLimitDetailResponse.CreditLimitDetail.MONTO_PAGO,
                        DatePayment = objGetCreditLimitDetailResponse.CreditLimitDetail.FEC_PAGO,
                        NumberPac = objGetCreditLimitDetailResponse.CreditLimitDetail.NRO_PAC
                    };

                }
            }
            return PartialView(oPostCreditLimitDetailModel);
        }

        public ActionResult AccountRelationPlan(string strIdSession, Areas.Dashboard.Models.Postpaid.DataCustomerModel objCustomer)
        {
            return PartialView(GetRelationPlans(strIdSession, objCustomer));
        }

        public ActionResult AccountSuspendedContract(string strIdSession)
        {
            List<HELPER.AccountSuspendedContract.AccountTypeSuspended> lstTypeSuspended = null;
            PostpaidService.SuspendedTypeResponseCommon objSuspendedTypeResponseCommon = null;
            PostpaidService.SuspendedTypeRequestCommon objSuspendedTypeRequestCommon = new PostpaidService.SuspendedTypeRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
            };

            try
            {

                objSuspendedTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<PostpaidService.SuspendedTypeResponseCommon>(() => { return objPostpaidService.GetSuspendedType(objSuspendedTypeRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSuspendedTypeRequestCommon.audit.Session, objSuspendedTypeRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(objSuspendedTypeRequestCommon.audit.transaction);
            }

            if (objSuspendedTypeResponseCommon != null && objSuspendedTypeResponseCommon.ListItem != null)
            {
                lstTypeSuspended = new List<HELPER.AccountSuspendedContract.AccountTypeSuspended>();
                foreach (PostpaidService.ListItem item in objSuspendedTypeResponseCommon.ListItem)
                {
                    lstTypeSuspended.Add(new HELPER.AccountSuspendedContract.AccountTypeSuspended()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }

            Areas.Dashboard.Models.PostpaidDataAccount.AccountSuspendedContractModel objPostTypeSuspendedModel = new Models.PostpaidDataAccount.AccountSuspendedContractModel()
            {
                lstTypeSuspendedModel = lstTypeSuspended
            };
            return PartialView(objPostTypeSuspendedModel);
        }

        public ActionResult AccountAnnotation(string strIdSession, string strType, string strplataforma)
        {
            Areas.Dashboard.Models.PostpaidDataAccount.AccountAnnotationModel objPostTypeAnnotationModel = null;
            PostpaidService.AnnotationTypeResponseCommon objAnnotationTypeResponseCommon = null;
            PostpaidService.AnnotationRequestCommon objAnnotationRequestCommon = new PostpaidService.AnnotationRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
                plataforma = strplataforma
            };

            try
            {
                objAnnotationTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<PostpaidService.AnnotationTypeResponseCommon>(() => { return objPostpaidService.GetAnnotationType(objAnnotationRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAnnotationRequestCommon.audit.Session, objAnnotationRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(objAnnotationRequestCommon.audit.transaction);
            }

            if (objAnnotationTypeResponseCommon != null)
            {
                objPostTypeAnnotationModel = new Models.PostpaidDataAccount.AccountAnnotationModel();
                foreach (PostpaidService.ListItem item in objAnnotationTypeResponseCommon.ListItem)
                {
                    objPostTypeAnnotationModel.lstTypeAnnotation.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountAnnotation.TypeAnnotation()
                    {
                        Code = item.Code,
                        Description = item.Description,
                    });
                }

                objPostTypeAnnotationModel.Type = strType;
            }
            return PartialView(objPostTypeAnnotationModel);
        }

        public ActionResult AccountSuspendedContractList()
        {

            return PartialView();
        }

        public JsonResult GetAccountSuspendedContractList(string strIdSession, string strReasonID, string strFechaIni, string strFechaFin)
        {
            PostpaidService.SuspendedContractResponsePostPaid objGetSuspendedContractResponse;
            List<HELPER.AccountSuspendedContract.AccountSuspendedContract> lstAccountSuspendedContract = null;
            Areas.Dashboard.Models.PostpaidDataAccount.AccountSuspendedContractModel objPostSuspendedContractModel = new Areas.Dashboard.Models.PostpaidDataAccount.AccountSuspendedContractModel();

            PostpaidService.SuspendedContractRequestPostPaid objGetSuspendedContractRequest = new PostpaidService.SuspendedContractRequestPostPaid()
            {
                ReasonID = strReasonID,
                StartDate = strFechaIni,
                EndDate = strFechaFin,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
            };

            if (!string.IsNullOrEmpty(strReasonID))
            {
                objGetSuspendedContractResponse = new PostpaidService.SuspendedContractResponsePostPaid();
                try
                {
                    objGetSuspendedContractResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.SuspendedContractResponsePostPaid>(() => { return objPostpaidService.GetSuspendedContract(objGetSuspendedContractRequest); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objGetSuspendedContractRequest.audit.Session, objGetSuspendedContractRequest.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objGetSuspendedContractRequest.audit.transaction);
                }

                if (objGetSuspendedContractResponse != null && objGetSuspendedContractResponse.ListContract != null)
                {
                    lstAccountSuspendedContract = new List<HELPER.AccountSuspendedContract.AccountSuspendedContract>();

                    int num = 0;

                    foreach (PostpaidService.ContractPostPaid item in objGetSuspendedContractResponse.ListContract)
                    {
                        num += 1;
                        lstAccountSuspendedContract.Add(new HELPER.AccountSuspendedContract.AccountSuspendedContract()
                        {
                            Number = num,
                            ClienteId = item.CODIGO_CLIENTE,
                            StateUmbral = item.ESTADO_UMBRAL,
                            DateActivation = item.FECHA_ACTIVACION,
                            DateSuspended = item.FECHA_SUSPENSION,
                            ReasonSuspended = item.MOTIVO_SUSPENSION,
                            RatePlan = item.PLAN_TARIFARIO,
                            BusinessName = item.RAZON_SOCIAL,
                            SimCard = item.SIM_CARD,
                            Telephone = item.TELEFONO,
                            CustomerType = item.TIPO_CLIENTE,
                            User = item.USUARIO
                        });
                    }
                    objPostSuspendedContractModel.lstAccountSuspendedContract = lstAccountSuspendedContract;
                }
            }

            JsonResult json = Json(new { data = objPostSuspendedContractModel });
            json.MaxJsonLength = 500000000;

            return json;
        }

        public ActionResult AccountSubAccount(string strIdSession, string strCustomerId, string strAccountId)
        {
            PostpaidService.SubAccountResponsePostPaid objGetSubAccountResponse;
            List<Areas.Dashboard.Models.Postpaid.DataAccountModel> lstPostDataAccount = null;
            int counter = 0;
            PostpaidService.SubAccountRequestPostPaid objGetSubAccountRequest = new PostpaidService.SubAccountRequestPostPaid()
            {
                CustomerID = strCustomerId,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };
            try
            {
                objGetSubAccountResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.SubAccountResponsePostPaid>(() => { return objPostpaidService.GetSubAccount(objGetSubAccountRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetSubAccountRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetSubAccountRequest.audit.transaction);
            }

            if (objGetSubAccountResponse != null)
            {
                lstPostDataAccount = new List<Areas.Dashboard.Models.Postpaid.DataAccountModel>();
                foreach (PostpaidService.AccountPostPaid item in objGetSubAccountResponse.ListSubAccount)
                {
                    lstPostDataAccount.Add(new Areas.Dashboard.Models.Postpaid.DataAccountModel()
                    {
                        Name = item.NOMBRE,
                        Balance = item.SALDO,
                        ExpirationDate = item.FECHA_EXPIRACION,
                        LastName = item.APELLIDOS,
                        AccountParent = item.CUENTA_PADRE,
                        AccountId = item.CUENTAID,
                        Level = item.NIVEL,
                        CustomerId = item.CUSTOMERID,
                    });
                    counter++;
                }
            }
            Areas.Dashboard.Models.Postpaid.DataAccountModel oPostSubAccountModel = new Areas.Dashboard.Models.Postpaid.DataAccountModel()
            {
                AccountId = strAccountId,
                lstPostDataAccount = lstPostDataAccount
            };

            return PartialView(oPostSubAccountModel);
        }

        public JsonResult AccountExportRelationPlan(string strIdSession, Areas.Dashboard.Models.Postpaid.DataCustomerModel objDatacustomer)
        {
            RELATIONPLAN_MODEL objRelationPlanModel = GetRelationPlans(strIdSession, objDatacustomer);
            List<int> lstHelperPlan = null;

            lstHelperPlan = ValidateRelationPlanItem(objRelationPlanModel.lstAccountRelationPlan);

            string strPath = string.Empty;

            if (objRelationPlanModel.objAccountBag.NumberLines != null && objRelationPlanModel.lstAccountBagDetail.Count > 0)
            {
                strPath = oExcelHelper.ExportExcel(objRelationPlanModel, TemplateExcel.CONST_RELATION_PLAN_CONBOLSA, lstHelperPlan);
            }
            else if (objRelationPlanModel.objAccountBag.NumberLines != null && objRelationPlanModel.lstAccountBagDetail.Count == 0)
            {
                strPath = oExcelHelper.ExportExcel(objRelationPlanModel, TemplateExcel.CONST_RELATION_PLAN_SOLOBOLSA_CAB, lstHelperPlan);
            }
            else if (objRelationPlanModel.objAccountBag.FlagBagService == 1 && objRelationPlanModel.lstAccountBagDetail.Count > 0)
            {
                strPath = oExcelHelper.ExportExcel(objRelationPlanModel, TemplateExcel.CONST_RELATION_PLAN_SOLOBOLSA_DET, lstHelperPlan);
            }
            else
            {
                strPath = oExcelHelper.ExportExcel(objRelationPlanModel, TemplateExcel.CONST_RELATION_PLAN_SINBOLSA, lstHelperPlan);
            }
            var json = Json(strPath);
            json.MaxJsonLength = 500000000;
            return json;
        }

        public ActionResult AccountRelationPlanHFCLTE(string strIdSession, string strCustomerId, string strAplication, string strContractID, string strState, string strPlan, string strAddress, string strTelephony, string strCable, string strInternet, string strPlataformaAT)
        {
            Areas.Dashboard.Models.PostpaidDataAccount.AccountRelationPlanHFCLTEModel oRelationPlanHFCLTEModel = new Areas.Dashboard.Models.PostpaidDataAccount.AccountRelationPlanHFCLTEModel();

            PostpaidService.RelationPlanHFCLTEResponsePostPaid objRelationPlanHFCLTEResponse = null;
            PostpaidService.RelationPlanHFCLTERequestPostPaid objRelationPlanHFCLTERequest = new PostpaidService.RelationPlanHFCLTERequestPostPaid()
            {
                CustomerId = strCustomerId,
                Aplication = strAplication,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };
            oRelationPlanHFCLTEModel.StrAplicacion = strAplication;

            Claro.Web.Logging.Info(objRelationPlanHFCLTERequest.audit.Session, objRelationPlanHFCLTERequest.audit.transaction, "Inicio - AccountRelationPlanHFCLTE");

            //RPB - PROY ONE FIJA
            if (strPlataformaAT == "TOBE") //oCustomer.objPostDataAccount.plataformaAT
            {
                Claro.Web.Logging.Info(objRelationPlanHFCLTERequest.audit.Session, objRelationPlanHFCLTERequest.audit.transaction, "TOBE - AccountRelationPlanHFCLTE");
                if (strAplication == "HFC")
                {
                    Claro.Web.Logging.Info(objRelationPlanHFCLTERequest.audit.Session, objRelationPlanHFCLTERequest.audit.transaction, "HFC - AccountRelationPlanHFCLTE");
                    oRelationPlanHFCLTEModel.lstHFC = new List<HELPER.AccountRelationPlanHFCLTE.RelationPlanHFCLTE>();
                    oRelationPlanHFCLTEModel.lstHFC.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountRelationPlanHFCLTE.RelationPlanHFCLTE()
                        {
                            Contract = strContractID, //oCustomer.ContractID,
                            State = strState, //oCustomer.StateAgreement,
                            PlanId = string.Empty,
                            PlanRate = strPlan, //oService.Plan,
                            ComboID = string.Empty,
                            Combo = string.Empty,
                            Discount = string.Empty,
                            InstallationAdress = strAddress, //oCustomer.OfficeAddress,
                            TelephoneFixed = strTelephony, //oService.TelephonyValue,
                            InternetFixed = strInternet, //oService.InternetValue,
                            ClaroTV = strCable //oService.CableValue
                        });
                    Claro.Web.Logging.Info(objRelationPlanHFCLTERequest.audit.Session, objRelationPlanHFCLTERequest.audit.transaction, "Lista lstHFC - AccountRelationPlanHFCLTE : " + oRelationPlanHFCLTEModel.lstHFC.Count.ToString());
                }
            }
            else
            {
                try
                {
                    objRelationPlanHFCLTEResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.RelationPlanHFCLTEResponsePostPaid>(() => { return objPostpaidService.GetRelationPlanHFCLTE(objRelationPlanHFCLTERequest); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objRelationPlanHFCLTERequest.audit.Session, objRelationPlanHFCLTERequest.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objRelationPlanHFCLTERequest.audit.transaction);
                }

                if (objRelationPlanHFCLTEResponse != null)
                {
                    if (objRelationPlanHFCLTEResponse.ListServiceGSMAccount != null)
                    {
                        foreach (PostpaidService.ServiceGSMAccountPostPaid item in objRelationPlanHFCLTEResponse.ListServiceGSMAccount)
                        {
                            oRelationPlanHFCLTEModel.lstGSM.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountRelationPlanHFCLTE.RelationPlanHFCLTE()
                            {
                                Telephone = item.NRO_SERV,
                                Contract = item.CONTRATO,
                                State = item.ESTADO,
                                PlanId = item.COD_PLAN,
                                PlanRate = item.PLAN_TARIFARIO,
                                ComboID = item.COD_COMBO,
                                Combo = item.COMBO,
                                Discount = item.DESCUENTO,
                                InstallationAdress = item.DIR_INSTALACION
                            });
                        }
                    }
                    if (objRelationPlanHFCLTEResponse.ListServiceHFCAccount != null && objRelationPlanHFCLTEResponse.ListServiceHFCAccount.Count > 0)
                    {
                        foreach (PostpaidService.ServiceHFCAccountPostPaid item in objRelationPlanHFCLTEResponse.ListServiceHFCAccount)
                        {
                            oRelationPlanHFCLTEModel.lstHFC.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountRelationPlanHFCLTE.RelationPlanHFCLTE()
                            {
                                Contract = item.CONTRATO,
                                State = item.ESTADO,
                                PlanId = item.COD_PLAN,
                                PlanRate = item.PLAN_TARIFARIO,
                                ComboID = item.COD_COMBO,
                                Combo = item.COMBO,
                                Discount = item.DESCUENTO,
                                InstallationAdress = item.DIR_INSTALACION,
                                TelephoneFixed = item.TELEFONIA_FIJA,
                                InternetFixed = item.INTERNET_FIJO,
                                ClaroTV = item.CLAROTV
                            });
                        }
                    }
                    if (objRelationPlanHFCLTEResponse.ListServiceLTEAccount != null && objRelationPlanHFCLTEResponse.ListServiceLTEAccount.Count > 0)
                    {
                        foreach (PostpaidService.ServiceLTEAccountPostPaid item in objRelationPlanHFCLTEResponse.ListServiceLTEAccount)
                        {
                            oRelationPlanHFCLTEModel.lstLTE.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountRelationPlanHFCLTE.RelationPlanHFCLTE()
                            {
                                Telephone = item.MSISDN,
                                Contract = item.CO_ID,
                                DateActivation = item.FECHA_ACTIVACION,
                                State = item.ESTADO,
                                DateState = item.FECHA_ESTADO,
                                StateReason = item.MOTIVO_ESTADO,
                                PlanId = item.COD_PLAN,
                                PlanRate = item.PLAN_TARIFARIO,
                                AgreementTerm = item.PLAZO_ACUERDO,
                                PointSale = item.PTO_VENTA,
                                Seller = item.VENDEDOR,
                                Bell = item.CAMPANA,
                                InternetFixed = item.INTERNET,
                                TelephoneFixed = item.TELEFONIA,
                                ClaroTV = item.CABLE_TV,
                                ProducType = item.TIPO_PROD,
                                ServiceType = item.TIPO_PLAN,
                            });
                        }
                    }

                    if (objRelationPlanHFCLTEResponse.ListEquipment != null)
                    {
                        foreach (PostpaidService.EquipmentPostPaid item in objRelationPlanHFCLTEResponse.ListEquipment)
                        {
                            oRelationPlanHFCLTEModel.lstEquiment.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountRelationPlanHFCLTE.Equiment()
                            {
                                Code = item.CODIGO,
                                Description = item.DESCRIPCION,
                                Series = item.SERIE,
                                Model = item.MODELO,
                                Price = item.PRECIO,
                                Bell = item.CAMPANA,
                                Combo = item.COMBO,
                                Pdv = item.PDV,
                                NroQuota = item.NRO_CUOTA,
                                AmountQuota = item.MONTO_CUOTA,
                                InitialAmountQuota = item.MONTO_INICIAL_CUOTA,
                                DateSale = item.FECHA_VENTA
                            });
                        }
                    }
                }
            }


            return PartialView(oRelationPlanHFCLTEModel);
        }

        public List<int> ValidateRelationPlanItem(List<HELPER.AccountRelationPlanHelper.AccountRelationPlan> objRelationPlan)
        {
            List<int> lstList = Enumerable.Range(7, 35).ToList();

            foreach (HELPER.AccountRelationPlanHelper.AccountRelationPlan item in objRelationPlan)
            {
                if (item.F_LDI == Claro.Constants.NumberOneString) { lstList.Remove(7); }
                if (item.F_InternationalRoaming == Claro.Constants.NumberOneString) { lstList.Remove(8); }
                if (item.F_TimData == Claro.Constants.NumberOneString) { lstList.Remove(9); }
                if (item.F_TimFax == Claro.Constants.NumberOneString) { lstList.Remove(10); }
                if (item.F_SMSPackage == Claro.Constants.NumberOneString) { lstList.Remove(11); }
                if (item.F_DataPackage == Claro.Constants.NumberOneString) { lstList.Remove(12); }
                if (item.F_LevelEnablement == Claro.Constants.NumberOneString) { lstList.Remove(13); }
                if (item.F_RPV == Claro.Constants.NumberOneString) { lstList.Remove(14); }
                if (item.F_CostRPV == Claro.Constants.NumberOneString) { lstList.Remove(15); }
                if (item.F_CostRPCUnlimited == Claro.Constants.NumberOneString) { lstList.Remove(16); }
                if (item.F_Insurance == Claro.Constants.NumberOneString) { lstList.Remove(17); }
                if (item.F_Loan == Claro.Constants.NumberOneString) { lstList.Remove(18); }
                if (item.F_TimProConnection == Claro.Constants.NumberOneString) { lstList.Remove(19); }
                if (item.F_GPRS == Claro.Constants.NumberOneString) { lstList.Remove(20); }
                if (item.F_LBS == Claro.Constants.NumberOneString) { lstList.Remove(21); }
                if (item.F_SolSMS == Claro.Constants.NumberOneString) { lstList.Remove(22); }
                if (item.F_RTP == Claro.Constants.NumberOneString) { lstList.Remove(23); }
                if (item.F_Habilitation == Claro.Constants.NumberOneString) { lstList.Remove(24); }
                if (item.F_SMSMail == Claro.Constants.NumberOneString) { lstList.Remove(25); }
                if (item.F_RPCUnlimited == Claro.Constants.NumberOneString) { lstList.Remove(26); }
                if (item.F_RechargePersonalAccount == Claro.Constants.NumberOneString) { lstList.Remove(27); }
                if (item.F_SMS == Claro.Constants.NumberOneString) { lstList.Remove(28); }
                if (item.F_MMS == Claro.Constants.NumberOneString) { lstList.Remove(29); }
                if (item.F_Blackberry == Claro.Constants.NumberOneString) { lstList.Remove(30); }
                if (item.F_Reposicion == Claro.Constants.NumberOneString) { lstList.Remove(31); }
                if (item.F_AdditionalConsumptionControl == Claro.Constants.NumberOneString) { lstList.Remove(32); lstList.Remove(33); }
                if (item.F_DetailedBilling == Claro.Constants.NumberOneString) { lstList.Remove(34); }
                if (item.F_DetailedBillingModA == Claro.Constants.NumberOneString) { lstList.Remove(35); }
                if (item.F_ClaroDirect == Claro.Constants.NumberOneString) { lstList.Remove(36); }
                if (item.F_DetailedBillingCharge == Claro.Constants.NumberOneString) { lstList.Remove(37); }
                if (item.F_ClaroFax == Claro.Constants.NumberOneString) { lstList.Remove(38); }
                if (item.F_ClaroData == Claro.Constants.NumberOneString) { lstList.Remove(39); }
                if (item.F_Clarobanking == Claro.Constants.NumberOneString) { lstList.Remove(40); }
                if (FlagBagService.ToString() == Claro.Constants.NumberOneString) { lstList.Remove(41); }
                break;
            }
            return lstList;
        }

        public JsonResult ExistAgreement_Origin()
        {
            string strRoute = "";

            strRoute = KEY.AppSettings("strRutaSiteSIGA");

            return Json(new { DataRuta = strRoute, });
        }
    }
}
