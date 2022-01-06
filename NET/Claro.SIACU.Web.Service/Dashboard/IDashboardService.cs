using Claro.SIACU.Entity.Dashboard.Board.GetCustomerInfo;
using Claro.SIACU.Entity.Dashboard.Board.GetPostpaidLines;
using Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts;
using Claro.SIACU.Entity.Dashboard.Board.GetPrepaidProducts;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetInvoice;
using System.Collections.Generic;
using System.ServiceModel;

namespace Claro.SIACU.Web.Service.Dashboard
{

    [ServiceContract]
    public interface IDashboardService
    {
        [OperationContract]
        InvoiceResponse GetIdOnBase(Entity.Dashboard.Postpaid.GetInvoice.InvoiceRequest objRequest);
        [OperationContract]
        CustomerInfoResponse GetCustomerInfo(CustomerInfoRequest oGetCustomerInfoRequest);

        [OperationContract]
        PostpaidProductsResponse GetPostpaidProducts(PostpaidProductsRequest GetPostpaidProductsRequest);

        [OperationContract]
        PostpaidLinesResponse GetPostpaidLines(PostpaidLinesRequest GetPostpaidLinesRequest);

        [OperationContract]
        PrepaidProductsResponse GetPrepaidProducts(PrepaidProductsRequest GetPrepaidProductsRequest);

        [OperationContract]
        [ServiceKnownType(typeof(Entity.Dashboard.ICustomer))]
        [ServiceKnownType(typeof(Entity.Dashboard.Postpaid.Customer))]
        [ServiceKnownType(typeof(Entity.Dashboard.Prepaid.Customer))]
        Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse GetCustomer(Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerRequest objCustomerRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.GetRedirectSession.RedirectSessionResponse GetRedirectSession(Claro.SIACU.Entity.Dashboard.Board.GetRedirectSession.RedirectSessionRequest objRedirectSessionRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.InsertRedirectCommunication.InsertRedirectComResponse InsertRedirectCommunication(Claro.SIACU.Entity.Dashboard.Board.InsertRedirectCommunication.InsertRedirectComRequest objInsertRedirectComRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.ValidateRedirectCommunication.ValidateRedirectComResponse ValidateRedirectCommunication(Claro.SIACU.Entity.Dashboard.Board.ValidateRedirectCommunication.ValidateRedirectComRequest objValidateRedirectComRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.GetInstants.InstantsResponse GetInstants(Claro.SIACU.Entity.Dashboard.Board.GetInstants.InstantsRequest objInstantsRequest);


        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.GetCustomerName.CustomerNameResponse GetCustomerName(Claro.SIACU.Entity.Dashboard.Board.GetCustomerName.CustomerNameRequest objCustomerNameRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.GetInvoiceFtp.InvoiceFtpResponse GetInvoiceFtp(Claro.SIACU.Entity.Dashboard.Board.GetInvoiceFtp.InvoiceFtpRequest objInvoiceFtpRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.GetFileInvoice.FileInvoiceResponse GetFileInvoice(Claro.SIACU.Entity.Dashboard.Board.GetFileInvoice.FileInvoiceRequest objFileInvoiceRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.GetReceiptNumber.ReceiptNumberResponse GetReceiptNumber(Claro.SIACU.Entity.Dashboard.Board.GetReceiptNumber.ReceiptNumberRequest ReceiptNumberRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.GetTypeMIME.TypeMIMEResponse GetTypeMIME(Claro.SIACU.Entity.Dashboard.Board.GetTypeMIME.TypeMIMERequest objGetTypeMIMERequest);


        [OperationContract]
        [ServiceKnownType(typeof(Entity.Dashboard.ICustomer))]
        [ServiceKnownType(typeof(Entity.Dashboard.Postpaid.Customer))]
        [ServiceKnownType(typeof(Entity.Dashboard.Prepaid.Customer))]
        Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse GetCustomerForInstMasive(Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerRequest objCustomerRequest);

        [OperationContract]
        Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse GetFileAjusteV3(Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationRequest objFileDefaultImpersonationRequest);

        [OperationContract]
        Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse GetFileAjuste(Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationRequest objFileDefaultImpersonationRequest);
        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.GetEmployeByUser.EmployeeResponse GetEmployeByUser(Entity.Dashboard.Board.GetEmployeByUser.EmployeeRequest objEmployeeRequest);
        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.ReadOptionsByUser.ReadOptionsByUserResponse ReadOptionsByUser(Entity.Dashboard.Board.ReadOptionsByUser.ReadOptionsByUserRequest objReadOptionsByUserRequest);
        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Board.CheckingUser.CheckingUserResponse CheckingUser(Entity.Dashboard.Board.CheckingUser.CheckingUserRequest objCheckingUserRequest);
        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjusteResponse GetFlagAjuste(Claro.SIACU.Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjsuteRequest objFlagAjsuteRequest);
        [OperationContract]
        string GetParameterByCode(string strIdSession, string strTransaction, int Code);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoResponse GetStateEquipo(Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoRequest objRequest);

        //PROY-140464 Ajustes - INI
        [OperationContract]
        Entity.Dashboard.Postpaid.GetReasonCancelInvoice.ReasonCancelInvoiceResponse GetReasonCancelInvoice(Entity.Dashboard.Postpaid.GetReasonCancelInvoice.ReasonCancelInvoiceRequest objRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.CancelInvoice.CancelInvoiceResponse CancelInvoice(Entity.Dashboard.Postpaid.CancelInvoice.CancelInvoiceRequest objRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.CancelInvoice.TypificationResponse GetTyficationList(Entity.Dashboard.Postpaid.CancelInvoice.TypificationRequest objRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.EvaluateAmount.EvaluateAmountResponse GetEvaluateAmount(Entity.Dashboard.Postpaid.EvaluateAmount.EvaluateAmountRequest objRequest);
        
        [OperationContract]
        Entity.Dashboard.Postpaid.GetDegradationTobe.GetDegradationResponseTobe GetBalancePostpaidConsumerB2ELegacyDegradation(Entity.Dashboard.Postpaid.GetDegradationTobe.GetDegradationRequestTobe request);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetUserProfile.UserProfileResponse GetProfileUser(Entity.Dashboard.Postpaid.GetUserProfile.UserProfileRequest objRequest);
        //PROY-140464 Ajustes - FIN
        [OperationContract]
        Entity.Dashboard.Postpaid.GetListaBloqueoDesbloqueo.Response.ResponseListaObtenerBloqueos GetListaBloqueoDesbloqueo(Entity.Dashboard.Postpaid.GetListaBloqueoDesbloqueo.Request.RequestListaobtenerBloqueos objRequest, Claro.Entity.AuditRequest objAuditoriaRequest);

        //INICIATIVA 616
        [OperationContract]
        List<Entity.Dashboard.Postpaid.Balance> GetBalanceFijaTobe(Claro.Entity.AuditRequest objAudit, string strMsidn, string coIdPub);


    }
}
