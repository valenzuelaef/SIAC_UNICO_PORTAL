using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.CustomerCreateNotUseHelper;
using KEY = Claro.ConfigurationManager;
using MODELS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class PrepaidDataCustomerController : Controller
    {

        public ActionResult CustomerSegment(string strIdSession, string strTypeDocument, string strNumberDocument, string strTelephoneCustomer)
        {
            List<MODELS.PrepaidDataCustomer.CustomerSegmentModel> listCustomerSegment = null;
            CommonService.SegmentsResponseCommon objSegmentsResponse;
            CommonService.SegmentsRequestCommon objSegmentsRequest = new CommonService.SegmentsRequestCommon
            {
                TypeDocument = strTypeDocument,
                NumberDocument = strNumberDocument,
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession)
            };

            try
            {
                objSegmentsResponse = Claro.Web.Logging.ExecuteMethod<CommonService.SegmentsResponseCommon>(() => { return new CommonService.CommonServiceClient().GetSegments(objSegmentsRequest); });
            }
            catch (Exception ex)
            {
                objSegmentsResponse = null;
                Claro.Web.Logging.Error(strIdSession, objSegmentsRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objSegmentsRequest.audit.transaction);
            }
            string strConsultDate = DateTime.Now.ToString("yyyyMMdd");


            if (objSegmentsResponse.code == "0")
            {
                SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
                try
                {
                    string msg = Claro.SIACU.Constants.MessageSegment + strConsultDate + Claro.SIACU.Constants.Telephone + strTelephoneCustomer;
                    Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strTelephoneCustomer, KEY.AppSettings("strAudiTraCodHistoSegmento"), msg); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
                }
            }
            if (objSegmentsResponse != null && objSegmentsResponse.ListSegment != null && objSegmentsResponse.ListSegment.Count > 0)
            {
                listCustomerSegment = new List<MODELS.PrepaidDataCustomer.CustomerSegmentModel>();
                foreach (CommonService.Segment item in objSegmentsResponse.ListSegment)
                {
                    listCustomerSegment.Add(new MODELS.PrepaidDataCustomer.CustomerSegmentModel()
                    {
                        DocumentIdentity = item.NRO_DOC,
                        LastUpgrade = Convert.ToDate(item.ULTIMAACTUALIZACION).ToString("dd/MM/yyyy"),
                        Segment = item.SEGMENTO
                    });
                }
            }

            return PartialView(listCustomerSegment);
        }


        public ActionResult CustomerQuery()
        {
            return PartialView();
        }


        public ActionResult CustomerPast(string strIdSession, string strTelephoneCustomer, string strFlagHistory, bool IsTFI)
        {
            PrepaidService.PreviousCustomersResponsePrePaid objPreviousCustomersResponse = null;
            List<MODELS.PrepaidDataCustomer.CustomerPastModel> listCustomerPast = null;
            PrepaidService.PreviousCustomersRequestPrePaid objPreviousCustomersRequest = null;

            try
            {
                strTelephoneCustomer = (IsTFI ? "000" + strTelephoneCustomer : strTelephoneCustomer);
                objPreviousCustomersRequest = new PrepaidService.PreviousCustomersRequestPrePaid()
                {
                    Telephone = strTelephoneCustomer.Trim(),
                    Account = "",
                    ContactId = "",
                    FlagRegistry = KEY.AppSettings("FlagRegistry"),
                    FlagGetAll = KEY.AppSettings("FlagGetAll"),
                    FlagHistory = strFlagHistory,
                    audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
                };
                objPreviousCustomersResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.PreviousCustomersResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetPreviousCustomers(objPreviousCustomersRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objPreviousCustomersRequest.audit.transaction, ex.Message);
            }

            if (objPreviousCustomersResponse != null)
            {
                listCustomerPast = new List<MODELS.PrepaidDataCustomer.CustomerPastModel>();
                if (objPreviousCustomersResponse.listCustomer != null)
                {
                    foreach (PrepaidService.CustomerPrePaid item in objPreviousCustomersResponse.listCustomer)
                    {
                        listCustomerPast.Add(new MODELS.PrepaidDataCustomer.CustomerPastModel()
                        {
                            Lastname = item.APELLIDOS,
                            Name = item.NOMBRES,
                            Reason = item.MOTIVOREGISTRO,
                            Date = item.FAX,
                            CustomerCode = item.OBJID_CONTACTO
                        });
                    }
                }
            }
            return PartialView(listCustomerPast);
        }



        public ActionResult CreateNotUser(string strIdSession)
        {
            MODELS.PrepaidDataCustomer.CustomerCreateNotUserModel objCustomerCreateNotUserModel = new MODELS.PrepaidDataCustomer.CustomerCreateNotUserModel()
            {
                listSex = GetSex(strIdSession),
                listMotiveRegister = GetMotiveRegister(strIdSession),
                listTypeDocumentCustomer = GetTypeDocumentCustomer(strIdSession),
                listStateCivil = GetStateCivil(strIdSession),
                listConfirmMail = GetConfirmMail(strIdSession),
                listOccupation = GetOccupation(strIdSession)

            };

            return View(objCustomerCreateNotUserModel);
        }


        private List<HELPER_ITEM.CreateNotUserItem> GetSex(string strIdSession)
        {
            List<HELPER_ITEM.CreateNotUserItem> listCreateNotUserItem = null;
            CommonService.SexResponseCommon objSexResponseCommon = null;
            CommonService.SexRequestCommon objSexRequestCommon = new CommonService.SexRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession)
            };
            try
            {
                objSexResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.SexResponseCommon>(() => { return new CommonService.CommonServiceClient().GetSex(objSexRequestCommon); });
            }
            catch (Exception ex)
            {
                objSexResponseCommon = null;
                Claro.Web.Logging.Error(strIdSession, objSexRequestCommon.audit.transaction, ex.Message);
            }

            if (objSexResponseCommon != null)
            {
                listCreateNotUserItem = new List<HELPER_ITEM.CreateNotUserItem>();
                foreach (CommonService.ListItem item in objSexResponseCommon.ListItem)
                {
                    listCreateNotUserItem.Add(new HELPER_ITEM.CreateNotUserItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }
            return listCreateNotUserItem;
        }


        private List<HELPER_ITEM.CreateNotUserItem> GetMotiveRegister(string strIdSession)
        {
            List<HELPER_ITEM.CreateNotUserItem> listCreateNotUserItem = null;
            CommonService.MotiveRegisterResponseCommon objMotiveRegisterResponseCommon = null;
            CommonService.MotiveRegisterRequestCommon objMotiveRegisterRequestCommon = new CommonService.MotiveRegisterRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession)
            };
            try
            {
                objMotiveRegisterResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.MotiveRegisterResponseCommon>(() => { return new CommonService.CommonServiceClient().GetMotiveRegister(objMotiveRegisterRequestCommon); });
            }
            catch (Exception ex)
            {
                objMotiveRegisterResponseCommon = null;
                Claro.Web.Logging.Error(strIdSession, objMotiveRegisterRequestCommon.audit.transaction, ex.Message);
            }

            if (objMotiveRegisterResponseCommon != null)
            {
                listCreateNotUserItem = new List<HELPER_ITEM.CreateNotUserItem>();
                foreach (CommonService.ListItem item in objMotiveRegisterResponseCommon.ListItem)
                {
                    listCreateNotUserItem.Add(new HELPER_ITEM.CreateNotUserItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }
            return listCreateNotUserItem;
        }

        private List<HELPER_ITEM.CreateNotUserItem> GetTypeDocumentCustomer(string strIdSession)
        {
            List<HELPER_ITEM.CreateNotUserItem> listCreateNotUserItem = null;
            CommonService.TypeDocumentCustomerResponseCommon objTypeDocumentCustomerResponseCommon = null;
            CommonService.TypeDocumentCustomerRequestCommon objTypeDocumentCustomerRequestCommon = new CommonService.TypeDocumentCustomerRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession)
            };
            try
            {
                objTypeDocumentCustomerResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.TypeDocumentCustomerResponseCommon>(() => { return new CommonService.CommonServiceClient().GetTypeDocumentCustomer(objTypeDocumentCustomerRequestCommon); });
            }
            catch (Exception ex)
            {
                objTypeDocumentCustomerResponseCommon = null;
                Claro.Web.Logging.Error(strIdSession, objTypeDocumentCustomerRequestCommon.audit.transaction, ex.Message);
            }

            if (objTypeDocumentCustomerResponseCommon != null)
            {
                listCreateNotUserItem = new List<HELPER_ITEM.CreateNotUserItem>();
                foreach (CommonService.ListItem item in objTypeDocumentCustomerResponseCommon.ListItem)
                {
                    listCreateNotUserItem.Add(new HELPER_ITEM.CreateNotUserItem()
                    {
                        Description = item.Description
                    });
                }
            }
            return listCreateNotUserItem;
        }

        private List<HELPER_ITEM.CreateNotUserItem> GetStateCivil(string strIdSession)
        {
            List<HELPER_ITEM.CreateNotUserItem> listCreateNotUserItem = null;
            CommonService.StateCivilResponseCommon objStateCivilResponseCommon = null;
            CommonService.StateCivilRequestCommon objStateCivilRequestCommon = new CommonService.StateCivilRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession)
            };
            try
            {
                objStateCivilResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.StateCivilResponseCommon>(() => { return new CommonService.CommonServiceClient().GetStateCivil(objStateCivilRequestCommon); });
            }
            catch (Exception ex)
            {
                objStateCivilResponseCommon = null;
                Claro.Web.Logging.Error(strIdSession, objStateCivilRequestCommon.audit.transaction, ex.Message);
            }

            if (objStateCivilResponseCommon != null)
            {
                listCreateNotUserItem = new List<HELPER_ITEM.CreateNotUserItem>();
                foreach (CommonService.ListItem item in objStateCivilResponseCommon.ListItem)
                {
                    listCreateNotUserItem.Add(new HELPER_ITEM.CreateNotUserItem()
                    {
                        Description = item.Description
                    });
                }
            }
            return listCreateNotUserItem;
        }

        private List<HELPER_ITEM.CreateNotUserItem> GetConfirmMail(string strIdSession)
        {
            List<HELPER_ITEM.CreateNotUserItem> listCreateNotUserItem = null;
            CommonService.ConfirmMailResponseCommon objConfirmMailResponseCommon = null;
            CommonService.ConfirmMailRequestCommon objConfirmMailRequestCommon = new CommonService.ConfirmMailRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession)
            };
            try
            {
                objConfirmMailResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.ConfirmMailResponseCommon>(() => { return new CommonService.CommonServiceClient().GetConfirmMail(objConfirmMailRequestCommon); });
            }
            catch (Exception ex)
            {
                objConfirmMailResponseCommon = null;
                Claro.Web.Logging.Error(strIdSession, objConfirmMailRequestCommon.audit.transaction, ex.Message);
            }

            if (objConfirmMailResponseCommon != null)
            {
                listCreateNotUserItem = new List<HELPER_ITEM.CreateNotUserItem>();
                foreach (CommonService.ListItem item in objConfirmMailResponseCommon.ListItem)
                {
                    listCreateNotUserItem.Add(new HELPER_ITEM.CreateNotUserItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }
            return listCreateNotUserItem;
        }

        private List<HELPER_ITEM.CreateNotUserItem> GetOccupation(string strIdSession)
        {
            List<HELPER_ITEM.CreateNotUserItem> listCreateNotUserItem = null;
            CommonService.OccupationResponseCommon objOccupationResponseCommon = null;
            CommonService.OccupationRequestCommon objOccupationRequestCommon = new CommonService.OccupationRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession)
            };
            try
            {
                objOccupationResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.OccupationResponseCommon>(() => { return new CommonService.CommonServiceClient().GetOccupation(objOccupationRequestCommon); });
            }
            catch (Exception ex)
            {
                objOccupationResponseCommon = null;
                Claro.Web.Logging.Error(strIdSession, objOccupationRequestCommon.audit.transaction, ex.Message);
            }

            if (objOccupationResponseCommon != null)
            {
                listCreateNotUserItem = new List<HELPER_ITEM.CreateNotUserItem>();
                foreach (CommonService.ListItem item in objOccupationResponseCommon.ListItem)
                {
                    listCreateNotUserItem.Add(new HELPER_ITEM.CreateNotUserItem()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }
            return listCreateNotUserItem;
        }


        public JsonResult RegisterNotUser(string strIdSession, string strModality, string strTypeProcess, string strTelephoneCustomer, string strRegistered,
                                          string strMotiveRegister, string strName, string strLastName, string strDateBirth,
                                          string strSex, string strTypeDocument, string strNumberDocument,
                                          string strStateCivil, string strOccupation, string strPosition,
                                          string strTelephoneReference, string strFax, string strEmail,
                                          string strAddress, string strDistrict, string strZipCode,
                                          string strUrbanization, string strCity, string strDepartment,
                                          string strReference, string strConfirmMail)
        {
            PrepaidService.CreateContactNotUSerResponsePrePaid objCreateContactNotUSerResponsePrePaid = null;
            PrepaidService.CreateContactNotUSerRequestPrePaid objCreateContactNotUSerRequestPrePaid = new PrepaidService.CreateContactNotUSerRequestPrePaid()
            {
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession),
                Modality = strModality,
                TypeProcess = strTypeProcess,
                TelephoneCustomer = strTelephoneCustomer,
                Registered = strRegistered,
                MotiveRegister = strMotiveRegister,
                Name = strName,
                LastName = strLastName,
                DateBirth = strDateBirth,
                Sex = strSex,
                TypeDocument = strTypeDocument,
                NumberDocument = strNumberDocument,
                StateCivil = strStateCivil,
                Occupation = strOccupation,
                Position = strPosition,
                TelephoneReference = strTelephoneReference,
                Fax = strFax,
                Email = strEmail,
                Address = strAddress,
                District = strDistrict,
                ZipCode = strZipCode,
                Urbanization = strUrbanization,
                City = strCity,
                Department = strDepartment,
                Reference = strReference,
                ConfirmMail = strConfirmMail

            };

            try
            {
                objCreateContactNotUSerResponsePrePaid = Claro.Web.Logging.ExecuteMethod<PrepaidService.CreateContactNotUSerResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().CreateContactNotUSer(objCreateContactNotUSerRequestPrePaid); });
            }
            catch (Exception ex)
            {
                objCreateContactNotUSerResponsePrePaid = null;
                Claro.Web.Logging.Error(strIdSession, objCreateContactNotUSerRequestPrePaid.audit.transaction, ex.Message);
            }

            return Json(new { flagUpdate = objCreateContactNotUSerResponsePrePaid.FlagUpdate, message = objCreateContactNotUSerResponsePrePaid.MessageText });
        }



    }
}