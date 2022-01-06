using Claro.SIACU.Entity.Dashboard.Postpaid.CancelInvoice;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace Claro.SIACU.Web.Service.Dashboard
{

    public class DashboardService : IDashboardService
    {



        public Claro.SIACU.Entity.Dashboard.Board.GetCustomerInfo.CustomerInfoResponse GetCustomerInfo(Claro.SIACU.Entity.Dashboard.Board.GetCustomerInfo.CustomerInfoRequest oGetCustomerInfoRequest)
        {

            Claro.SIACU.Entity.Dashboard.Board.GetCustomerInfo.CustomerInfoResponse objCustomerInfoResponse = null;

            try
            {
                objCustomerInfoResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetCustomerInfo.CustomerInfoResponse>(() => { return Business.Dashboard.Dashboard.GetCustomerInfo(oGetCustomerInfoRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oGetCustomerInfoRequest.Audit.Session, oGetCustomerInfoRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objCustomerInfoResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts.PostpaidProductsResponse GetPostpaidProducts(Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts.PostpaidProductsRequest objPostpaidProductsRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts.PostpaidProductsResponse objPostpaidProductsResponse = null;

            try
            {
                objPostpaidProductsResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts.PostpaidProductsResponse>(
                    objPostpaidProductsRequest.Audit.Session,
                    objPostpaidProductsRequest.Audit.Transaction,
                    () => { return Business.Dashboard.Dashboard.GetPostpaidProducts(objPostpaidProductsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPostpaidProductsRequest.Audit.Session, objPostpaidProductsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objPostpaidProductsResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Board.GetPostpaidLines.PostpaidLinesResponse GetPostpaidLines(Claro.SIACU.Entity.Dashboard.Board.GetPostpaidLines.PostpaidLinesRequest objPostpaidLinesRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetPostpaidLines.PostpaidLinesResponse objPostpaidProductsResponse = null;

            try
            {
                objPostpaidProductsResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetPostpaidLines.PostpaidLinesResponse>(() => { return Business.Dashboard.Dashboard.GetPostpaidLines(objPostpaidLinesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPostpaidLinesRequest.Audit.Session, objPostpaidLinesRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objPostpaidProductsResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Board.GetPrepaidProducts.PrepaidProductsResponse GetPrepaidProducts(Claro.SIACU.Entity.Dashboard.Board.GetPrepaidProducts.PrepaidProductsRequest objPrepaidProductsRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetPrepaidProducts.PrepaidProductsResponse objPrepaidProductsResponse = null;

            try
            {
                objPrepaidProductsResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetPrepaidProducts.PrepaidProductsResponse>(() => { return Business.Dashboard.Dashboard.GetPrepaidProducts(objPrepaidProductsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPrepaidProductsRequest.Audit.Session, objPrepaidProductsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objPrepaidProductsResponse;
        }


        public Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse GetCustomer(Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerRequest objCustomerRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse objCustomerResponse = null;
            try
            {
                objCustomerResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse>(() => { return Business.Dashboard.Dashboard.GetCustomer(objCustomerRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
                throw new FaultException(ex.Message);
            }
            return objCustomerResponse;
        }



        public Claro.SIACU.Entity.Dashboard.Board.GetRedirectSession.RedirectSessionResponse GetRedirectSession(Claro.SIACU.Entity.Dashboard.Board.GetRedirectSession.RedirectSessionRequest objRedirectSessionRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetRedirectSession.RedirectSessionResponse objRedirectSessionResponse = null;

            try
            {
                objRedirectSessionResponse = Business.Dashboard.Dashboard.GetRedirectSession(objRedirectSessionRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRedirectSessionRequest.Audit.Session, objRedirectSessionRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objRedirectSessionResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Board.InsertRedirectCommunication.InsertRedirectComResponse InsertRedirectCommunication(Claro.SIACU.Entity.Dashboard.Board.InsertRedirectCommunication.InsertRedirectComRequest objInsertRedirectComRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.InsertRedirectCommunication.InsertRedirectComResponse onjInsertRedirectComResponse = null;
            try
            {
                onjInsertRedirectComResponse = Business.Dashboard.Dashboard.InsertRedirectCommunication(objInsertRedirectComRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertRedirectComRequest.Audit.Session, objInsertRedirectComRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return onjInsertRedirectComResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Board.ValidateRedirectCommunication.ValidateRedirectComResponse ValidateRedirectCommunication(Claro.SIACU.Entity.Dashboard.Board.ValidateRedirectCommunication.ValidateRedirectComRequest objValidateRedirectRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.ValidateRedirectCommunication.ValidateRedirectComResponse onjValidateRedirectComResponse = null;
            try
            {
                onjValidateRedirectComResponse = Business.Dashboard.Dashboard.ValidateRedirectCommunication(objValidateRedirectRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objValidateRedirectRequest.Audit.Session, objValidateRedirectRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return onjValidateRedirectComResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Board.GetInstants.InstantsResponse GetInstants(Claro.SIACU.Entity.Dashboard.Board.GetInstants.InstantsRequest objInstantsRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetInstants.InstantsResponse objInstantsResponse = null;

            try
            {
                objInstantsResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetInstants.InstantsResponse>(() => { return Business.Dashboard.Dashboard.GetInstants(objInstantsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInstantsRequest.Audit.Session, objInstantsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objInstantsResponse;

        }
        public Claro.SIACU.Entity.Dashboard.Board.GetCustomerName.CustomerNameResponse GetCustomerName(Claro.SIACU.Entity.Dashboard.Board.GetCustomerName.CustomerNameRequest objCustomerNameRequest)
        {

            Claro.SIACU.Entity.Dashboard.Board.GetCustomerName.CustomerNameResponse objCustomerNameResponse = null;

            try
            {
                objCustomerNameResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetCustomerName.CustomerNameResponse>(() => { return Business.Dashboard.Dashboard.GetCustomerName(objCustomerNameRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCustomerNameRequest.Audit.Session, objCustomerNameRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objCustomerNameResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Board.GetInvoiceFtp.InvoiceFtpResponse GetInvoiceFtp(Claro.SIACU.Entity.Dashboard.Board.GetInvoiceFtp.InvoiceFtpRequest objInvoiceFtpRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetInvoiceFtp.InvoiceFtpResponse objInvoiceFtpResponse = null;

            try
            {
                objInvoiceFtpResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetInvoiceFtp.InvoiceFtpResponse>(1, () => { return Business.Dashboard.Dashboard.GetInvoiceFtp(objInvoiceFtpRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInvoiceFtpRequest.Audit.Session, objInvoiceFtpRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objInvoiceFtpResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Board.GetFileInvoice.FileInvoiceResponse GetFileInvoice(Claro.SIACU.Entity.Dashboard.Board.GetFileInvoice.FileInvoiceRequest objFileInvoiceRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetFileInvoice.FileInvoiceResponse objFileInvoiceResponse = null;

            try
            {
                objFileInvoiceResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetFileInvoice.FileInvoiceResponse>(() => { return Business.Dashboard.Dashboard.GetFileInvoice(objFileInvoiceRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFileInvoiceRequest.Audit.Session, objFileInvoiceRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFileInvoiceResponse;
        }

        public Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse GetFileDefaultImpersonation(Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationRequest objFileDefaultImpersonationRequest)
        {
            Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse objFileDefaultImpersonationResponse = null;

            try
            {
                objFileDefaultImpersonationResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse>(1, () => { return Business.Dashboard.Dashboard.GetFileDefaultImpersonation(objFileDefaultImpersonationRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFileDefaultImpersonationRequest.Audit.Session, objFileDefaultImpersonationRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFileDefaultImpersonationResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Board.GetTypeMIME.TypeMIMEResponse GetTypeMIME(Claro.SIACU.Entity.Dashboard.Board.GetTypeMIME.TypeMIMERequest objGetTypeMIMERequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetTypeMIME.TypeMIMEResponse objGetTypeMIMEResponse = null;

            try
            {
                objGetTypeMIMEResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetTypeMIME.TypeMIMEResponse>(() => { return Business.Dashboard.Dashboard.GetTypeMIME(objGetTypeMIMERequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetTypeMIMERequest.Audit.Session, objGetTypeMIMERequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objGetTypeMIMEResponse;
        }


        public Claro.SIACU.Entity.Dashboard.Board.GetReceiptNumber.ReceiptNumberResponse GetReceiptNumber(Claro.SIACU.Entity.Dashboard.Board.GetReceiptNumber.ReceiptNumberRequest ReceiptNumberRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetReceiptNumber.ReceiptNumberResponse ReceiptNumberResponse = null;

            try
            {
                ReceiptNumberResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetReceiptNumber.ReceiptNumberResponse>(() => { return Business.Dashboard.Dashboard.GetReceiptNumber(ReceiptNumberRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(ReceiptNumberRequest.Audit.Session, ReceiptNumberRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return ReceiptNumberResponse;
        }


        public Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse GetCustomerForInstMasive(Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerRequest objCustomerRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse objCustomerResponse = null;
            try
            {
                objCustomerResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse>(() => { return Business.Dashboard.Dashboard.GetCustomerInstantMasive(objCustomerRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objCustomerResponse;
        }



        public Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse GetFileAjuste(Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationRequest objFileDefaultImpersonationRequest)
        {


            Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse objFileDefaultImpersonationResponse = new Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse();
            try
            {
                objFileDefaultImpersonationResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse>(1, () => { return Business.Dashboard.Dashboard.GetFileAjuste(objFileDefaultImpersonationRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFileDefaultImpersonationRequest.Audit.Session, objFileDefaultImpersonationRequest.Audit.Transaction, ex.Message);
            }
            return objFileDefaultImpersonationResponse;


        }
        public Claro.SIACU.Entity.Dashboard.Board.CheckingUser.CheckingUserResponse CheckingUser(Entity.Dashboard.Board.CheckingUser.CheckingUserRequest objCheckingUserRequest)
        {


            Claro.SIACU.Entity.Dashboard.Board.CheckingUser.CheckingUserResponse objCheckingUserResponse = new Claro.SIACU.Entity.Dashboard.Board.CheckingUser.CheckingUserResponse();
            try
            {
                objCheckingUserResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.CheckingUser.CheckingUserResponse>(() => { return Business.Dashboard.Dashboard.CheckingUser(objCheckingUserRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCheckingUserRequest.Audit.Session, objCheckingUserRequest.Audit.Transaction, ex.Message);
            }
            return objCheckingUserResponse;


        }
        public Claro.SIACU.Entity.Dashboard.Board.ReadOptionsByUser.ReadOptionsByUserResponse ReadOptionsByUser(Entity.Dashboard.Board.ReadOptionsByUser.ReadOptionsByUserRequest objReadOptionsByUserRequest)
        {


            Claro.SIACU.Entity.Dashboard.Board.ReadOptionsByUser.ReadOptionsByUserResponse objReadOptionsByUserResponse = new Claro.SIACU.Entity.Dashboard.Board.ReadOptionsByUser.ReadOptionsByUserResponse();
            try
            {
                objReadOptionsByUserResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.ReadOptionsByUser.ReadOptionsByUserResponse>(() => { return Business.Dashboard.Dashboard.ReadOptionsByUser(objReadOptionsByUserRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objReadOptionsByUserRequest.Audit.Session, objReadOptionsByUserRequest.Audit.Transaction, ex.Message);
            }
            return objReadOptionsByUserResponse;


        }
        public Claro.SIACU.Entity.Dashboard.Board.GetEmployeByUser.EmployeeResponse GetEmployeByUser(Entity.Dashboard.Board.GetEmployeByUser.EmployeeRequest objEmployeeRequest)
        {


            Claro.SIACU.Entity.Dashboard.Board.GetEmployeByUser.EmployeeResponse objEmployeeResponse = new Claro.SIACU.Entity.Dashboard.Board.GetEmployeByUser.EmployeeResponse();
            try
            {
                objEmployeeResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Board.GetEmployeByUser.EmployeeResponse>(() => { return Business.Dashboard.Dashboard.GetEmployeByUser(objEmployeeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objEmployeeRequest.Audit.Session, objEmployeeRequest.Audit.Transaction, ex.Message);
            }
            return objEmployeeResponse;


        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjusteResponse GetFlagAjuste(Claro.SIACU.Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjsuteRequest objFlagAjsuteRequest)
        {


            Claro.SIACU.Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjusteResponse objFlagAjusteResponse = new Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjusteResponse();
            try
            {
                objFlagAjusteResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjusteResponse>(() => { return Business.Dashboard.Dashboard.GetFlagAjuste(objFlagAjsuteRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFlagAjsuteRequest.Audit.Session, objFlagAjsuteRequest.Audit.Transaction, ex.Message);
            }
            return objFlagAjusteResponse;


        }

        public string GetParameterByCode(string strIdSession, string strTransaction, int Code)
        {
            string value = string.Empty;
            try
            {
                value = Claro.Web.Logging.ExecuteMethod<string>(() => { return Business.Dashboard.Dashboard.GetParameterByCode(strIdSession, strTransaction, Code); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            return value;
        }

        public Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse GetFileAjusteV3(Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationRequest objFileDefaultImpersonationRequest)
        {
            Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse objFileDefaultImpersonationResponse = new Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse();
            try
            {
                objFileDefaultImpersonationResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse>(1, () => { return Business.Dashboard.Dashboard.GetFileAjusteV3(objFileDefaultImpersonationRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFileDefaultImpersonationRequest.Audit.Session, objFileDefaultImpersonationRequest.Audit.Transaction, ex.Message);
            }
            return objFileDefaultImpersonationResponse;
        }

        public Entity.Dashboard.Postpaid.GetInvoice.InvoiceResponse GetIdOnBase(Entity.Dashboard.Postpaid.GetInvoice.InvoiceRequest objRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetInvoice.InvoiceResponse objResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetInvoice.InvoiceResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetInvoice.InvoiceResponse>(1, () => { return Business.Dashboard.Postpaid.GetIdOnBase(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }
        public Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoResponse GetStateEquipo(Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoResponse>(() =>
                {
                    return Business.Dashboard.Postpaid.GetStateEquipo(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

    
        public Entity.Dashboard.Postpaid.GetDegradationTobe.GetDegradationResponseTobe GetBalancePostpaidConsumerB2ELegacyDegradation(Entity.Dashboard.Postpaid.GetDegradationTobe.GetDegradationRequestTobe request)
        {
            Entity.Dashboard.Postpaid.GetDegradationTobe.GetDegradationResponseTobe response = null;
            try
            {
                response = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetDegradationTobe.GetDegradationResponseTobe>(() =>
                {
                    return Business.Dashboard.Postpaid.GetBalancePostpaidConsumerB2ELegacyDegradation(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(request.Audit.Session, request.Audit.Transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
                throw;
            }

            return response;
        }

        //PROY-140464 Ajustes - INI
        public Entity.Dashboard.Postpaid.GetReasonCancelInvoice.ReasonCancelInvoiceResponse GetReasonCancelInvoice(Entity.Dashboard.Postpaid.GetReasonCancelInvoice.ReasonCancelInvoiceRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetReasonCancelInvoice.ReasonCancelInvoiceResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetReasonCancelInvoice.ReasonCancelInvoiceResponse>(() =>
                {
                    return Business.Dashboard.Postpaid.GetReasonCancelInvoice(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public Entity.Dashboard.Postpaid.CancelInvoice.CancelInvoiceResponse CancelInvoice(Entity.Dashboard.Postpaid.CancelInvoice.CancelInvoiceRequest objRequest)
        {
            Entity.Dashboard.Postpaid.CancelInvoice.CancelInvoiceResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.CancelInvoice.CancelInvoiceResponse>(() =>
                {
                    return Business.Dashboard.Postpaid.CancelInvoice(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public TypificationResponse GetTyficationList(TypificationRequest objRequest)
        {
            TypificationResponse objTypificationResponse = null;
            try
            {
                objTypificationResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Dashboard.Postpaid.GetTyficationList(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objTypificationResponse;
        }

        public Entity.Dashboard.Postpaid.EvaluateAmount.EvaluateAmountResponse GetEvaluateAmount(Entity.Dashboard.Postpaid.EvaluateAmount.EvaluateAmountRequest objRequest)
        {
            Entity.Dashboard.Postpaid.EvaluateAmount.EvaluateAmountResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.EvaluateAmount.EvaluateAmountResponse>(() =>
                {
                    return Business.Dashboard.Postpaid.GetEvaluateAmount(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public Entity.Dashboard.Postpaid.GetUserProfile.UserProfileResponse GetProfileUser(Entity.Dashboard.Postpaid.GetUserProfile.UserProfileRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetUserProfile.UserProfileResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetUserProfile.UserProfileResponse>(() =>
                {
                    return Business.Dashboard.Postpaid.GetProfileUser(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
        //PROY-140464 Ajustes - FIN


        public Entity.Dashboard.Postpaid.GetListaBloqueoDesbloqueo.Response.ResponseListaObtenerBloqueos GetListaBloqueoDesbloqueo(Entity.Dashboard.Postpaid.GetListaBloqueoDesbloqueo.Request.RequestListaobtenerBloqueos objRequest, Claro.Entity.AuditRequest objAuditoriaRequest)
        {
            Entity.Dashboard.Postpaid.GetListaBloqueoDesbloqueo.Response.ResponseListaObtenerBloqueos objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetListaBloqueoDesbloqueo.Response.ResponseListaObtenerBloqueos>(() =>
                {
                    return Business.Dashboard.Postpaid.GetListaBloqueoDesbloqueo(objRequest, objAuditoriaRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objAuditoriaRequest.Transaction, objAuditoriaRequest.Session, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objResponse;
        }


        //INICIATIVA 616
        public List<Entity.Dashboard.Postpaid.Balance> GetBalanceFijaTobe(Claro.Entity.AuditRequest objAudit, string strMsidn, string coIdPub)
        {
            List<Entity.Dashboard.Postpaid.Balance> objResponse = new List<Entity.Dashboard.Postpaid.Balance>();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Balance>>(() =>
                    {
                        return Business.Dashboard.Postpaid.GetBalanceFijaTobe(objAudit, strMsidn, coIdPub);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objAudit.Transaction, objAudit.Session, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objResponse;
        }
    }
}
