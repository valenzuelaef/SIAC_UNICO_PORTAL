using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Entity.Dashboard.Postpaid;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class PostpaidDataCustomerController : Controller
    {

        readonly PostpaidService.PostpaidServiceClient objService = new PostpaidService.PostpaidServiceClient();
        readonly CommonService.CommonServiceClient objCommon = new CommonService.CommonServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.DashboardService.DashboardServiceClient oServiceDashboard = new DashboardService.DashboardServiceClient();
        

        public ActionResult CustomerData(string strIdSession, string strDocumentNumber) 
        {
                SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
                try
                {
                    string strConsultDate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                    string strText = Claro.SIACU.Constants.AllTypeClient + Claro.SIACU.Constants.DateHours + strConsultDate;
                    Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strDocumentNumber, KEY.AppSettings("strAudiTraCodDatosCliente"), strText); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
                }
            return PartialView();
        }

        public ActionResult AddressOffice(string strIdSession, string strDocumentType, string strDocumentNumber)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer.CustomerOfficeAddressModel objOfficeAddressModel = new Models.PostpaidDataCustomer.CustomerOfficeAddressModel();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
          
            if (!string.IsNullOrEmpty(strDocumentType) && !string.IsNullOrEmpty(strDocumentNumber))
            {
                try
                {
                    string strDocumentCode = DocumentCode(strDocumentType, strDocumentNumber);
                    PostpaidService.OfficeAddressPostPaid objAddress = GetDataAddress(strDocumentCode, strDocumentNumber, audit);
                    if (objAddress != null)
                    {
                        objOfficeAddressModel = DataAddressOffice(objAddress);
                    }
                   
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }

            }
            return PartialView(objOfficeAddressModel);
        }

        private PostpaidService.OfficeAddressPostPaid GetDataAddress(string strDocumentCode, string strDocumentNumber, PostpaidService.AuditRequest objAudit)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.OfficeAddressRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.OfficeAddressRequestPostPaid()
            {
                DocumentType = strDocumentCode,
                DocumentNumber = strDocumentNumber,
                audit = objAudit
            };

            Claro.SIACU.Web.WebApplication.PostpaidService.OfficeAddressResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.OfficeAddressResponsePostPaid>(
                objRequest.audit.Session,
                objRequest.audit.transaction,
                () =>
                { return objService.GetAddressOfficce(objRequest); });
            return objResponse.ObjOfficeAddress;
        }

        private Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer.CustomerOfficeAddressModel DataAddressOffice(PostpaidService.OfficeAddressPostPaid objAdress)
        {
            return new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer.CustomerOfficeAddressModel()
            {
                BusinessName = objAdress.RAZON_SOCIAL,
                InvoiceAddress = objAdress.DIRECCION_FACT
            };
        }

        public string DocumentCode(string strDocumentType, string strDocumentNumber)
        {
            string strDocumentCode = "";

            switch (strDocumentType)
            {
                case Claro.SIACU.Constants.DNI:
                case Claro.SIACU.Constants.RUC:
                    switch (strDocumentNumber.ToString().Length)
                    {
                        case Claro.Constants.NumberEleven:
                            strDocumentCode = Claro.Constants.NumberZeroSixString;
                            break;
                        case Claro.Constants.NumberEight:
                            strDocumentCode = Claro.Constants.NumberZeroOneString;
                            break;
                    }
                    break;
                case Claro.SIACU.Constants.Passport:
                    strDocumentCode = Claro.Constants.NumberZeroSevenString;
                    break;
                case Claro.SIACU.Constants.CardForeign_:
                    strDocumentCode = Claro.Constants.NumberZeroFourString;
                    break;
                default:

                    strDocumentCode = Claro.Constants.NumberOneNegativeString;
                    break;
            }

            return strDocumentCode;
        }

        public ActionResult CustomerSegment(string strIdSession, string strDocumentType, string strDocumentNumber)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer.CustomerHistorySegmentModel objHistorySegmentModel = new Models.PostpaidDataCustomer.CustomerHistorySegmentModel();
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            if (!string.IsNullOrEmpty(strDocumentType) && !string.IsNullOrEmpty(strDocumentNumber))
            {
                try
                {
                    List<CommonService.Segment> lstSegment = GetSegment(strDocumentType, strDocumentNumber, audit);
                    objHistorySegmentModel = CustomerSegment(lstSegment);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);

                }


            }

            return PartialView(objHistorySegmentModel);
        }

        private List<CommonService.Segment> GetSegment(string strTypeDocument, string strDocumentNumber, CommonService.AuditRequest objAudit)
        {
            CommonService.SegmentsRequestCommon objSegmentsRequest = new CommonService.SegmentsRequestCommon
            {
                TypeDocument = strTypeDocument,
                NumberDocument = strDocumentNumber,
                audit = objAudit
            };

            CommonService.SegmentsResponseCommon objResponse = Claro.Web.Logging.ExecuteMethod<CommonService.SegmentsResponseCommon>(
                objSegmentsRequest.audit.Session,
                objSegmentsRequest.audit.transaction,
                () =>
                { return objCommon.GetSegments(objSegmentsRequest); });

            return objResponse.ListSegment;
        }

        private Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer.CustomerHistorySegmentModel CustomerSegment(List<CommonService.Segment> lstSegment)
        {

            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer.CustomerHistorySegmentModel objHistorySegmentModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer.CustomerHistorySegmentModel();

            if (lstSegment != null && lstSegment.Count > 0)
            {
                foreach (CommonService.Segment item in lstSegment)
                {
                    objHistorySegmentModel.lstCustomerHistorySegment.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CustomerHistorySegment.CustomerHistorySegment()
                    {
                        DocumentNumber = item.NRO_DOC,
                        LastUpdate = Convert.ToDate(item.ULTIMAACTUALIZACION).ToString("dd/MM/yyyy"),
                        Segment = item.SEGMENTO
                    });
                }
            }

            return objHistorySegmentModel;
        }

        public ActionResult Contact()
        {
            return PartialView();
        }

        public JsonResult GetContact(string strIdSession, string strAccount, string strTelephone, string stroptionPermissions)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer.ContactModel objContactModel = null;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            if (!string.IsNullOrEmpty(strAccount))
            {
                try
                {
                    Claro.SIACU.Web.WebApplication.PostpaidService.ContactResponsePostPaid objResponse = GetContact(strAccount, audit);
                    objContactModel = ContactModel(objResponse, strAccount, strTelephone);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }
            }

            return Json(new { data = objContactModel });
        }

        private Claro.SIACU.Web.WebApplication.PostpaidService.ContactResponsePostPaid GetContact(string Account, PostpaidService.AuditRequest audit)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.ContactRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ContactRequestPostPaid()
            {
                audit = audit,
                DocumentCode = Claro.Constants.LetterR,
                Type = Claro.Constants.NumberNineHundredTwentyNineString,
                CustomerCode = Account
            };

            Claro.SIACU.Web.WebApplication.PostpaidService.ContactResponsePostPaid objResponse =
            Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ContactResponsePostPaid>(
            objRequest.audit.Session,
            objRequest.audit.transaction,
            () =>
            { return objService.GetContact(objRequest); });
            return objResponse;
        }

        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer.ContactModel ContactModel(Claro.SIACU.Web.WebApplication.PostpaidService.ContactResponsePostPaid objContactResponsePostPaid, string strAccount, string strTelephone)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer.ContactModel objContactModel = new Models.PostpaidDataCustomer.ContactModel()
            {
                AvailableEmails = objContactResponsePostPaid.AvailableEmails,
                DocumentTypeDNI = objContactResponsePostPaid.DocumentTypeDNI,
                DocumentTypeRUC = objContactResponsePostPaid.DocumentTypeRUC,
                DocumentTypePAS = objContactResponsePostPaid.DocumentTypePAS,
                DocumentTypeCEX = objContactResponsePostPaid.DocumentTypeCEX,
                DocumentTypeCIP = objContactResponsePostPaid.DocumentTypeCIP,
                DocumentTypeCFA = objContactResponsePostPaid.DocumentTypeCFA,
                AccountNumber = strAccount,
                Profile = Claro.SIACU.Constants.NM,
                NewState = objContactResponsePostPaid.NewState,
                ModifyState = objContactResponsePostPaid.ModifyState,
                ConsultState = objContactResponsePostPaid.ConsultState,
                Telephone = strTelephone,
                ConsultConst = objContactResponsePostPaid.ConsultConst,
                AddConst = objContactResponsePostPaid.AddConst,
                EditConst = objContactResponsePostPaid.EditConst,
                AddEditConst = objContactResponsePostPaid.AddEditConst,
            };

            #region list
            if (objContactResponsePostPaid.lstContactFields != null)
                foreach (Claro.SIACU.Web.WebApplication.PostpaidService.ContactFieldPostPaid item in objContactResponsePostPaid.lstContactFields)
                {
                    objContactModel.lstContactTypeField.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.ContactTypeFieldModel()
                    {
                        TCCCN_CODIGO = item.TCCCN_CODIGO,
                        TCCCV_NOMBRE = item.TCCCV_NOMBRE,
                        TCCCN_LONGITUD = item.TCCCN_LONGITUD,
                        TCCCN_TIPODATO = item.TCCCN_TIPODATO,
                        TCCCN_TIPODATOOPC = item.TCCCN_TIPODATOOPC,
                        SPERV_NOMBRE = item.SPERV_NOMBRE,
                        SPERV_TIPODATO = item.SPERV_TIPODATO,
                        TCCCC_EDITABLE = item.TCCCC_EDITABLE,
                        TCCCN_ORDEN = item.TCCCN_ORDEN,
                    });
                }

            if (objContactResponsePostPaid.lstContact != null)
            {
                Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.Contact objContact;
                foreach (Claro.SIACU.Web.WebApplication.PostpaidService.ContactPostPaid item in objContactResponsePostPaid.lstContact)
                {
                    objContact = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.Contact()
                    {
                        CSCTN_CODIGO = item.CSCTN_CODIGO,
                        SPERN_CODIGO = item.SPERN_CODIGO,
                        SOLIN_CODIGO = item.SOLIN_CODIGO,
                        TCCTN_CODIGO = item.TCCTN_CODIGO,
                        SPERV_ESTADO = item.SPERV_ESTADO,
                        SPERV_USU_CREA = item.SPERV_USU_CREA,
                        SPERD_FEC_REG = item.SPERD_FEC_REG,
                        SPERV_NOMBRE = item.SPERV_NOMBRE,
                        SPERV_APEPATERNO = item.SPERV_APEPATERNO,
                        SPERV_APEMATERNO = item.SPERV_APEMATERNO,
                        SPERV_CARGO = item.SPERV_CARGO,
                        SPERV_NUM_DOC = item.SPERV_NUM_DOC,
                        SPERV_TEL1 = item.SPERV_TEL1,
                        TDOCC_CODIGO = item.TDOCC_CODIGO,
                        SPERC_TIPO = item.SPERC_TIPO,
                        TDOCV_DESCRIPCION = item.TDOCV_DESCRIPCION,
                        P_CUSTOMER_ID = item.P_CUSTOMER_ID,
                        P_CUSTCODE = item.P_CUSTCODE,
                        P_SOLIN_CODIGO = item.P_SOLIN_CODIGO,
                        P_CSCTN_CODIGO = item.P_CSCTN_CODIGO,
                    };

                    if (item.lstAdditional != null)
                    {
                        foreach (var obj in item.lstAdditional)
                        {
                            objContact.CamposAdicionales.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.AdditionalFields()
                            {
                                TCXCN_CODIGO = obj.TCXCN_CODIGO,
                                TCCCN_CODIGO = obj.TCCCN_CODIGO,
                                TCCVV_VALOR = obj.TCCVV_VALOR,
                                TCCVN_CODIGO = obj.TCCVN_CODIGO,
                                SPERN_CODIGO = obj.SPERN_CODIGO
                            });
                        }
                    }

                    objContactModel.lstContact.Add(objContact);
                }
            }

            if (objContactResponsePostPaid.lstDataType != null)
            {
                foreach (Claro.SIACU.Web.WebApplication.PostpaidService.DataTypePostPaid item in objContactResponsePostPaid.lstDataType)
                {
                    objContactModel.lstDataType.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.DataTypeModel()
                    {
                        Codigo = item.Codigo,
                        Descripcion = item.Descripcion,

                    });
                }
            }

            if (objContactResponsePostPaid.lstContactType != null)
            {
                foreach (Claro.SIACU.Web.WebApplication.PostpaidService.ContactTypePostPaid item in objContactResponsePostPaid.lstContactType)
                {
                    objContactModel.lstContactType.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.ContactTypeModel()
                    {
                        TCCTN_CODIGO = item.TCCTN_CODIGO,
                        TCCTN_ORDEN = item.TCCTN_ORDEN,
                        TCCTV_NOMBRE = item.TCCTV_NOMBRE,
                        TCCTV_SISACT_SIAC_DES = item.TCCTV_SISACT_SIAC_DES,
                        TCCTV_OBLIGATORIO_DES = item.TCCTV_OBLIGATORIO_DES,
                        TCCT_ESTADO_DES = item.TCCT_ESTADO_DES,
                        TCCTC_ESTADO = item.TCCTC_ESTADO,
                        TCCTC_COPIABLE = item.TCCTC_COPIABLE,
                        TCCTC_OBLIGATORIO = item.TCCTC_OBLIGATORIO,
                        TCCTI_MINREGISTROS = item.TCCTI_MINREGISTROS,
                        TCCTI_MAXREGISTROS = item.TCCTI_MAXREGISTROS,
                        TCCTC_VISIBLESIACPOST = item.TCCTC_VISIBLESIACPOST,
                        TCCTC_VISIBLESISACT = item.TCCTC_VISIBLESISACT,
                    });
                }
            }

            if (objContactResponsePostPaid.lstDocumentType != null)
            {
                foreach (Claro.SIACU.Web.WebApplication.PostpaidService.DocumentTypePostPaid item in objContactResponsePostPaid.lstDocumentType)
                {
                    objContactModel.lstDocumentType.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.DocumentTypeModel()
                    {
                        Codigo = item.Codigo,
                        Descripcion = item.Descripcion,

                    });
                }
            }
            #endregion


            return objContactModel;
        }

        public JsonResult GetContactTypeByFiel(string strIdSession, int intCode)
        {
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            Claro.SIACU.Web.WebApplication.PostpaidService.ContactTypeByFieldResponsePostPaid objResponse = GetColumnsConfiguration(intCode, audit);
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.ContactTypeByFielModel objContactTypeByFielModel = ContactTypeByFielModel(objResponse);
            return Json(new { value = objContactTypeByFielModel.lstContactTypeByFiel });
        }

        private Claro.SIACU.Web.WebApplication.PostpaidService.ContactTypeByFieldResponsePostPaid GetColumnsConfiguration(int intCode, PostpaidService.AuditRequest objAudit)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.ContactTypeByFieldRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ContactTypeByFieldRequestPostPaid()
            {
                audit = objAudit,
                Code = intCode,
            };

            Claro.SIACU.Web.WebApplication.PostpaidService.ContactTypeByFieldResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ContactTypeByFieldResponsePostPaid>(
                objRequest.audit.Session,
                objRequest.audit.transaction,
                () =>
                { return objService.GetColumnsConfiguration(objRequest); });

            return objResponse;
        }

        private Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.ContactTypeByFielModel ContactTypeByFielModel(Claro.SIACU.Web.WebApplication.PostpaidService.ContactTypeByFieldResponsePostPaid objResponse)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.ContactTypeByFielModel objModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.ContactTypeByFielModel();

            if (objResponse.lstContactTypeByField != null)
            {
                foreach (var item in objResponse.lstContactTypeByField)
                {
                    objModel.lstContactTypeByFiel.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact.ContactTypeByFielModel()
                    {
                        TCXCN_CODIGO = item.TCXCN_CODIGO,
                        TCCCN_CODIGO = item.TCCCN_CODIGO,
                        TCXCC_OBLIGATORIO = item.TCXCC_OBLIGATORIO,
                        TCCCC_EDITABLE = item.TCCCC_EDITABLE,
                        TCCCN_ORDEN = item.TCCCN_ORDEN
                    });
                }
            }

            return objModel;
        }

        public JsonResult ContactSave(string strIdSession, string strSave, string strCuenta, string strtelephone, string strDelete)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.ContactSaveResponsePostPaid objResponse;
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

            try
            {
                objResponse = ContactSave(strIdSession, audit, strSave, strCuenta, strtelephone, strDelete);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            return Json(new { data = objResponse.ResponseMessage });
        }

        private Claro.SIACU.Web.WebApplication.PostpaidService.ContactSaveResponsePostPaid ContactSave(string strIdSession, PostpaidService.AuditRequest audit, string strSave, string strCuenta, string strtelephone, string strDelete)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.ContactSaveRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ContactSaveRequestPostPaid()
            {
                audit = audit,
                Save = strSave,
                Account = strCuenta,
                Telephone = strtelephone,
                Delete = strDelete,
            };

            Claro.SIACU.Web.WebApplication.PostpaidService.ContactSaveResponsePostPaid objResponse;

            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
            
            if (!string.IsNullOrEmpty(strDelete))
            {
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strtelephone, KEY.AppSettings("strAudiTraCodEliminarDatosContacto"), Claro.Constants.LetterE); });
            }

            Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strtelephone, KEY.AppSettings("strAudiTraCodGuardarDatosContacto"), Claro.Constants.LetterI); });

            objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ContactSaveResponsePostPaid>(() =>
            { return objService.ContactSave(objRequest); });

            return objResponse;
        }

        public JsonResult GetDataHistory(string strIdSession, string strCustomerID, string plataforma,string flagconvivencia)
        {
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

            List<PostpaidService.DataHistorical> lista = new List<PostpaidService.DataHistorical>();

            try
            {
                lista = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return objService.getDataHistory(audit, strIdSession, audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, strCustomerID, plataforma, flagconvivencia);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }

            return Json(new { data = lista });
        }
        public JsonResult GetCustomerChangeData(string strIdSession, string strTransaction, string strTelefono, string strCustomerId)
        {

            string cambiarfecha = "";
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método Portal: GetCustomerChangeData");

            Claro.SIACU.Web.WebApplication.PostpaidService.CustomerRequestPostPaid objRequestCustomer = new Claro.SIACU.Web.WebApplication.PostpaidService.CustomerRequestPostPaid();
            Claro.SIACU.Web.WebApplication.PostpaidService.CustomerResponsePostPaid objResponse = new Claro.SIACU.Web.WebApplication.PostpaidService.CustomerResponsePostPaid();
            
            try
            {
                objRequestCustomer = new Claro.SIACU.Web.WebApplication.PostpaidService.CustomerRequestPostPaid()
                {
                    audit = audit,
                    NumCellphone = strTelefono,
                    AccountCustomer = strCustomerId
                };

                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return objService.GetDataCustomer2(objRequestCustomer);
                });

                if (objResponse.ObjCustomer.FECHA_NAC != null)
                {
                    cambiarfecha = objResponse.ObjCustomer.FECHA_NAC;
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }

            return Json(new { objCus = objResponse.ObjCustomer });
        }
    }
}