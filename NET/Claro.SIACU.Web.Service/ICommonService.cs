using Claro.SIACU.Entity.Management.GetInstant;
using Claro.SIACU.Entity.Management.GetListInstant;
using System.ServiceModel;

namespace Claro.SIACU.Web.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICommonService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICommonService
    {
        [OperationContract]
        Entity.Common.GetSegments.SegmentsResponse GetSegments(Entity.Common.GetSegments.SegmentsRequest objSegmentsRequest);

        [OperationContract]
        Entity.Common.GetGroupBagList.GroupBagListResponse GetGroupBagList(Claro.SIACU.Entity.Common.GetGroupBagList.GroupBagLisRequest objGroupBagLisRequest);

        [OperationContract]
        Entity.Common.GetBagList.BagListResponse GetBagList(Entity.Common.GetBagList.BagListResquest objobjBagListResquest);

        [OperationContract]
        Entity.Common.GetSearchType.SearchTypeResponse GetSearchTypeList(Entity.Common.GetSearchType.SearchTypeRequest objSearchTypeRequest);

        [OperationContract]
        Entity.Common.GetPortability.PortabilityResponse GetPortability(Entity.Common.GetPortability.PortabilityRequest objPortabilityRequest);

        [OperationContract]
        Entity.Common.GetTypeDocumentCustomer.TypeDocumentCustomerResponse GetTypeDocumentCustomer(Entity.Common.GetTypeDocumentCustomer.TypeDocumentCustomerRequest objTypeDocumentCustomerRequest);

        [OperationContract]
        Claro.SIACU.Entity.Common.GetStateCivil.StateCivilResponse GetStateCivil(Claro.SIACU.Entity.Common.GetStateCivil.StateCivilRequest objStateCivilRequest);

        [OperationContract]
        Claro.SIACU.Entity.Common.GetSex.SexResponse GetSex(Claro.SIACU.Entity.Common.GetSex.SexRequest objSexRequest);

        [OperationContract]
        Claro.SIACU.Entity.Common.GetConfirmMail.ConfirmMailResponse GetConfirmMail(Claro.SIACU.Entity.Common.GetConfirmMail.ConfirmMailRequest objConfirmMailRequest);

        [OperationContract]
        Claro.SIACU.Entity.Common.GetMotiveRegister.MotiveRegisterResponse GetMotiveRegister(Claro.SIACU.Entity.Common.GetMotiveRegister.MotiveRegisterRequest objMotiveRegisterRequest);

        [OperationContract]
        Claro.SIACU.Entity.Common.GetOccupation.OccupationResponse GetOccupation(Claro.SIACU.Entity.Common.GetOccupation.OccupationRequest objOccupationRequest);

        [OperationContract]
        Entity.Common.GetStateType.StateTypeResponse GetStateType(Entity.Common.GetStateType.StateTypeRequest objStateTypeRequest);

        [OperationContract]
        Entity.Common.GetTransactionType.TransactionTypeResponse GetTransactionType(Entity.Common.GetTransactionType.TransactionTypeRequest objTransactionTypeRequest);

        [OperationContract]
        Entity.Common.GetCacDacType.CacDacTypeResponse GetCacDacType(Entity.Common.GetCacDacType.CacDacTypeRequest objCacDacTypeRequest);

        [OperationContract]
        Entity.Common.GetTelephoneType.TelephoneTypeResponse GetTelephoneType(Entity.Common.GetTelephoneType.TelephoneTypeRequest objTelephoneTypeRequest);

        [OperationContract]
        Entity.Management.GetListInstant.ListInstantResponse ListInstant(ListInstantRequest objListInstantRequestRequest);

        [OperationContract]
        Entity.Management.GetInstant.InstantResponse InsertInstant(InstantRequest objInsertInstantRequest);

        [OperationContract]
        Entity.Management.GetInstantById.InstantByIdResponse GetinstantById(Entity.Management.GetInstantById.InstantByIdRequest objInstantByIdRequest);

        [OperationContract]
        Entity.Management.GetInstant.InstantResponse UpdateInstant(InstantRequest objUpdateInstantRequest);

        [OperationContract]
        Entity.Management.GetInstant.InstantResponse DeactivateInstant(InstantRequest objDeactivateInstantRequest);

        [OperationContract]
        Entity.Management.GetBanner.BannerResponse GetBanner(Entity.Management.GetBanner.BannerRequest request);

        [OperationContract]
        Entity.Management.GetCreate.CreateResponse GetCreate(Entity.Management.GetCreate.CreateRequest request);

        [OperationContract]
        Entity.Management.GetModify.ModifyResponse GetModify(Entity.Management.GetModify.ModifyRequest request);

        [OperationContract]
        Entity.Management.GetBannerId.BannerIdResponse GetBannerId(Entity.Management.GetBannerId.BannerIdRequest request);

        [OperationContract]
        Entity.Management.GetDeactivate.DeactivateResponse GetDeactivate(Entity.Management.GetDeactivate.DeactivateRequest request);

        [OperationContract]
        Entity.Management.GetInstant.InstantResponse InsertInstants(InstantRequest objInsertInstantRequest);
        [OperationContract]
        Entity.Common.GetDocumentType.DocumentTypeResponse GetDocumentType(Entity.Common.GetDocumentType.DocumentTypeResquest request);

        [OperationContract]
        Entity.Management.GetInstant.InstantResponse GetInstantCorrelative(InstantRequest objInsertInstantRequest, string strLogin);

        [OperationContract]
        Entity.Management.GetInstant.InstantResponse SaveIntantsMasive(InstantRequest objInsertInstantRequest);

        [OperationContract]
        Entity.Common.GetEventType.EventTypeResponse GetEventType(Claro.SIACU.Entity.Common.GetEventType.EventTypeRequest objEventTypeRequest);

        [OperationContract]
        Entity.Common.GetFlatCode.FlatCodeResponse GetFlatCode(Entity.Common.GetFlatCode.FlatCodeRequest request);

        [OperationContract]
        Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsResponse GetDescriptions(Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsRequest objGetDescriptionsRequest);
    }
}
