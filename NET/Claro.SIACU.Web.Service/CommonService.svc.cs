using Claro.SIACU.Entity.Management.GetInstant;
using Claro.SIACU.Entity.Management.GetListInstant;
using System;
using System.ServiceModel;

namespace Claro.SIACU.Web.Service
{
    public class CommonService : ICommonService
    {
        /// <summary>
        ///  Obtiene una lista de los clientes historicos por medio del numero de documento
        /// </summary>
        /// <param name="strTypeDocument">Tipo de Documento</param>
        /// <param name="strNumberDocument">Numero de Documento</param>
        /// <returns>List List<Entity.Dashboard.Prepaid.Segment></returns>
        public Entity.Common.GetSegments.SegmentsResponse GetSegments(Entity.Common.GetSegments.SegmentsRequest objSegmentsRequest)
        {
            Entity.Common.GetSegments.SegmentsResponse objSegmentsResponse = null;
            try
            {
                objSegmentsResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetSegments.SegmentsResponse>(() => { return Business.Common.GetSegments(objSegmentsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSegmentsRequest.Audit.Session, objSegmentsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objSegmentsResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Entity.Common.GetGroupBagList.GroupBagListResponse GetGroupBagList(Claro.SIACU.Entity.Common.GetGroupBagList.GroupBagLisRequest objGroupBagLisRequest)
        {
            Entity.Common.GetGroupBagList.GroupBagListResponse objGroupBagListResponse = null;
            try
            {
                objGroupBagListResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetGroupBagList.GroupBagListResponse>(() => { return Business.Common.GetGroupBagList(objGroupBagLisRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGroupBagLisRequest.Audit.Session, objGroupBagLisRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objGroupBagListResponse;
        }

        public Entity.Common.GetEventType.EventTypeResponse GetEventType(Claro.SIACU.Entity.Common.GetEventType.EventTypeRequest objEventTypeRequest)
        {
            Entity.Common.GetEventType.EventTypeResponse objEventTypeResponse = null;
            try
            {
                objEventTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetEventType.EventTypeResponse>(() => { return Business.Common.GetEventType(objEventTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objEventTypeRequest.Audit.Session, objEventTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objEventTypeResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Entity.Common.GetBagList.BagListResponse GetBagList(Entity.Common.GetBagList.BagListResquest objobjBagListResquest)
        {
            Entity.Common.GetBagList.BagListResponse objBagListResponse;

            try
            {
                objBagListResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetBagList.BagListResponse>(() => { return Business.Common.GetBagList(objobjBagListResquest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objobjBagListResquest.Audit.Session, objobjBagListResquest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objBagListResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Entity.Common.GetSearchType.SearchTypeResponse GetSearchTypeList(Entity.Common.GetSearchType.SearchTypeRequest objSearchTypeRequest)
        {
            Entity.Common.GetSearchType.SearchTypeResponse objSearchTypeResponse = null;
            try
            {
                objSearchTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetSearchType.SearchTypeResponse>(() => { return Business.Common.GetSearchTypeList(objSearchTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSearchTypeRequest.Audit.Session, objSearchTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objSearchTypeResponse;
        }

        public Entity.Common.GetPortability.PortabilityResponse GetPortability(Entity.Common.GetPortability.PortabilityRequest objPortabilityRequest)
        {
            Entity.Common.GetPortability.PortabilityResponse objPortabilityTypeResponse;

            try
            {
                objPortabilityTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetPortability.PortabilityResponse>(() => { return Business.Common.GetPortability(objPortabilityRequest); });
            }
            catch (Exception ex)
            {
                objPortabilityTypeResponse = null;
                Claro.Web.Logging.Error(objPortabilityRequest.Audit.Session, objPortabilityRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objPortabilityTypeResponse;
        }


        public Entity.Common.GetTypeDocumentCustomer.TypeDocumentCustomerResponse GetTypeDocumentCustomer(Entity.Common.GetTypeDocumentCustomer.TypeDocumentCustomerRequest objTypeDocumentCustomerRequest)
        {
            Entity.Common.GetTypeDocumentCustomer.TypeDocumentCustomerResponse objTypeDocumentCustomerResponse = null;

            try
            {
                objTypeDocumentCustomerResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetTypeDocumentCustomer.TypeDocumentCustomerResponse>(() => { return Business.Common.GetTypeDocumentCustomer(objTypeDocumentCustomerRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypeDocumentCustomerRequest.Audit.Session, objTypeDocumentCustomerRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objTypeDocumentCustomerResponse;
        }



        public Claro.SIACU.Entity.Common.GetStateCivil.StateCivilResponse GetStateCivil(Claro.SIACU.Entity.Common.GetStateCivil.StateCivilRequest objStateCivilRequest)
        {
            Claro.SIACU.Entity.Common.GetStateCivil.StateCivilResponse objStateCivilResponse = null;

            try
            {
                objStateCivilResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Common.GetStateCivil.StateCivilResponse>(() => { return Business.Common.GetStateCivil(objStateCivilRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objStateCivilRequest.Audit.Session, objStateCivilRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objStateCivilResponse;
        }


        public Claro.SIACU.Entity.Common.GetSex.SexResponse GetSex(Claro.SIACU.Entity.Common.GetSex.SexRequest objSexRequest)
        {
            Claro.SIACU.Entity.Common.GetSex.SexResponse objSexResponse = null;
            try
            {
                objSexResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Common.GetSex.SexResponse>(() => { return Business.Common.GetSex(objSexRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSexRequest.Audit.Session, objSexRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objSexResponse;
        }


        public Claro.SIACU.Entity.Common.GetConfirmMail.ConfirmMailResponse GetConfirmMail(Claro.SIACU.Entity.Common.GetConfirmMail.ConfirmMailRequest objConfirmMailRequest)
        {
            Claro.SIACU.Entity.Common.GetConfirmMail.ConfirmMailResponse objConfirmMailResponse = null;
            try
            {
                objConfirmMailResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Common.GetConfirmMail.ConfirmMailResponse>(() => { return Business.Common.GetConfirmMail(objConfirmMailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConfirmMailRequest.Audit.Session, objConfirmMailRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objConfirmMailResponse;
        }


        public Claro.SIACU.Entity.Common.GetMotiveRegister.MotiveRegisterResponse GetMotiveRegister(Claro.SIACU.Entity.Common.GetMotiveRegister.MotiveRegisterRequest objMotiveRegisterRequest)
        {
            Claro.SIACU.Entity.Common.GetMotiveRegister.MotiveRegisterResponse objMotiveRegisterResponse = null;
            try
            {
                objMotiveRegisterResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Common.GetMotiveRegister.MotiveRegisterResponse>(() => { return Business.Common.GetMotiveRegister(objMotiveRegisterRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMotiveRegisterRequest.Audit.Session, objMotiveRegisterRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objMotiveRegisterResponse;
        }


        public Claro.SIACU.Entity.Common.GetOccupation.OccupationResponse GetOccupation(Claro.SIACU.Entity.Common.GetOccupation.OccupationRequest objOccupationRequest)
        {
            Claro.SIACU.Entity.Common.GetOccupation.OccupationResponse objOccupationResponse = null;
            try
            {
                objOccupationResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Common.GetOccupation.OccupationResponse>(() => { return Business.Common.GetOccupation(objOccupationRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objOccupationRequest.Audit.Session, objOccupationRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objOccupationResponse;
        }

        public Entity.Common.GetStateType.StateTypeResponse GetStateType(Entity.Common.GetStateType.StateTypeRequest objStateTypeRequest)
        {
            Entity.Common.GetStateType.StateTypeResponse objStateTypeResponse;

            try
            {
                objStateTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetStateType.StateTypeResponse>(() => { return Business.Common.GetStateType(objStateTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objStateTypeRequest.Audit.Session, objStateTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objStateTypeResponse;
        }

        public Entity.Common.GetTransactionType.TransactionTypeResponse GetTransactionType(Entity.Common.GetTransactionType.TransactionTypeRequest objTransactionTypeRequest)
        {
            Entity.Common.GetTransactionType.TransactionTypeResponse objTransactionTypeResponse;

            try
            {
                objTransactionTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetTransactionType.TransactionTypeResponse>(() => { return Business.Common.GetTransactionType(objTransactionTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTransactionTypeRequest.Audit.Session, objTransactionTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objTransactionTypeResponse;
        }

        public Entity.Common.GetCacDacType.CacDacTypeResponse GetCacDacType(Entity.Common.GetCacDacType.CacDacTypeRequest objCacDacTypeRequest)
        {
            Entity.Common.GetCacDacType.CacDacTypeResponse objCacDacTypeResponse;

            try
            {
                objCacDacTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetCacDacType.CacDacTypeResponse>(() => { return Business.Common.GetCacDacType(objCacDacTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCacDacTypeRequest.Audit.Session, objCacDacTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objCacDacTypeResponse;
        }

        public Entity.Management.GetInstant.InstantResponse InsertInstant(InstantRequest objInsertInstantRequest)
        {
            Entity.Management.GetInstant.InstantResponse objInsertInstantResponse = null;
            try
            {
                objInsertInstantResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetInstant.InstantResponse>(() => { return Business.Management.Common.InsertInstant(objInsertInstantRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objInsertInstantResponse;
        }

        public Entity.Management.GetInstant.InstantResponse UpdateInstant(InstantRequest objUpdateInstantRequest)
        {
            Entity.Management.GetInstant.InstantResponse objUpdateInstantResponse = null;
            try
            {
                objUpdateInstantResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetInstant.InstantResponse>(() => { return Business.Management.Common.UpdateInstant(objUpdateInstantRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objUpdateInstantRequest.Audit.Session, objUpdateInstantRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objUpdateInstantResponse;
        }
        
        public Entity.Management.GetInstant.InstantResponse DeactivateInstant(InstantRequest objDeactivateInstantRequest)
        {
            Entity.Management.GetInstant.InstantResponse objDeactivateInstantResponse = null;
            try
            {
                objDeactivateInstantResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetInstant.InstantResponse>(() => { return Business.Management.Common.DeactivateInstant(objDeactivateInstantRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDeactivateInstantRequest.Audit.Session, objDeactivateInstantRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objDeactivateInstantResponse;
        }

        public Entity.Common.GetTelephoneType.TelephoneTypeResponse GetTelephoneType(Entity.Common.GetTelephoneType.TelephoneTypeRequest objTelephoneTypeRequest)
        {
            Entity.Common.GetTelephoneType.TelephoneTypeResponse objTelephoneTypeResponse = null;
            try
            {
                objTelephoneTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetTelephoneType.TelephoneTypeResponse>(objTelephoneTypeRequest.Audit.Session, objTelephoneTypeRequest.Audit.Transaction, () => { return Business.Common.GetTelephoneType(objTelephoneTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTelephoneTypeRequest.Audit.Session, objTelephoneTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objTelephoneTypeResponse;
        }
       
        public Entity.Management.GetListInstant.ListInstantResponse ListInstant(ListInstantRequest objListInstantRequestRequest)
        {
            Entity.Management.GetListInstant.ListInstantResponse objListInstantResponse = null;
            try
            {
                objListInstantResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetListInstant.ListInstantResponse>(() => { return Business.Management.Common.ListInstant(objListInstantRequestRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListInstantRequestRequest.Audit.Session, objListInstantRequestRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objListInstantResponse;
        }
        
        public Entity.Management.GetInstantById.InstantByIdResponse GetinstantById(Entity.Management.GetInstantById.InstantByIdRequest objInstantByIdRequest)
        {
            Entity.Management.GetInstantById.InstantByIdResponse objInstantByIdResponse = null;
            try
            {
                objInstantByIdResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetInstantById.InstantByIdResponse>(() => { return Business.Management.Common.GetinstantById(objInstantByIdRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInstantByIdRequest.Audit.Session, objInstantByIdRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objInstantByIdResponse;
        }

        public Entity.Management.GetBanner.BannerResponse GetBanner(Entity.Management.GetBanner.BannerRequest request)
        {
            Entity.Management.GetBanner.BannerResponse objGetBannerResponse = null;
            try
            {
                objGetBannerResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetBanner.BannerResponse>(() => { return Business.Management.Common.GetBanner(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objGetBannerResponse;
        }

        public Entity.Management.GetCreate.CreateResponse GetCreate(Entity.Management.GetCreate.CreateRequest request)
        {
            Entity.Management.GetCreate.CreateResponse objGetCreateResponse = null;
            try
            {
                objGetCreateResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetCreate.CreateResponse>(() => { return Business.Management.Common.GetCreate(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objGetCreateResponse;
        }

        public Entity.Management.GetModify.ModifyResponse GetModify(Entity.Management.GetModify.ModifyRequest request)
        {
            Entity.Management.GetModify.ModifyResponse objGetModifyResponse = null;
            try
            {
                objGetModifyResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetModify.ModifyResponse>(() => { return Business.Management.Common.GetModify(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objGetModifyResponse;
        }

        public Entity.Management.GetBannerId.BannerIdResponse GetBannerId(Entity.Management.GetBannerId.BannerIdRequest request)
        {
            Entity.Management.GetBannerId.BannerIdResponse objGetBannerIdResponse = null;
            try
            {
                objGetBannerIdResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetBannerId.BannerIdResponse>(() => { return Business.Management.Common.GetBannerId(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objGetBannerIdResponse;
        }

        public Entity.Management.GetDeactivate.DeactivateResponse GetDeactivate(Entity.Management.GetDeactivate.DeactivateRequest request)
        {
            Entity.Management.GetDeactivate.DeactivateResponse objGetDeactivateResponse = null;
            try
            {
                objGetDeactivateResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetDeactivate.DeactivateResponse>(() => { return Business.Management.Common.GetDeactivate(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objGetDeactivateResponse;
        }


        public Entity.Management.GetInstant.InstantResponse InsertInstants(InstantRequest objInsertInstantRequest)
        {
            Entity.Management.GetInstant.InstantResponse objInsertInstantResponse = null;
            try
            {
                objInsertInstantResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetInstant.InstantResponse>(() => { return Business.Management.Common.InsertInstants(objInsertInstantRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objInsertInstantResponse;
        }
        public Entity.Common.GetDocumentType.DocumentTypeResponse GetDocumentType(Entity.Common.GetDocumentType.DocumentTypeResquest request)
        {
            Entity.Common.GetDocumentType.DocumentTypeResponse objAnnotationTypeResponse;
            try
            {
                objAnnotationTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetDocumentType.DocumentTypeResponse>(request.Audit.Session, request.Audit.Transaction, () => { return Business.Common.GetDocumentType(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objAnnotationTypeResponse;

        }

        public Entity.Management.GetInstant.InstantResponse GetInstantCorrelative(InstantRequest objInsertInstantRequest, string strLogin) 
        {
            Entity.Management.GetInstant.InstantResponse objInsertInstantResponse = null;
            try
            {
                objInsertInstantResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetInstant.InstantResponse>(() => { return Business.Management.Common.GetInstantCorrelative(objInsertInstantRequest, strLogin); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objInsertInstantResponse;
        }

        public Entity.Management.GetInstant.InstantResponse SaveIntantsMasive(InstantRequest objInsertInstantRequest) 
        {
            Entity.Management.GetInstant.InstantResponse objInsertInstantResponse = null;
            try 
            {
                objInsertInstantResponse = Claro.Web.Logging.ExecuteMethod<Entity.Management.GetInstant.InstantResponse>(() => { return Business.Management.Common.SaveIntantsMasive(objInsertInstantRequest); });
            }
            catch (Exception ex) 
            {
                Claro.Web.Logging.Error(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objInsertInstantResponse;
        }

        public Entity.Common.GetFlatCode.FlatCodeResponse GetFlatCode(Entity.Common.GetFlatCode.FlatCodeRequest request)
        {
            Entity.Common.GetFlatCode.FlatCodeResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () => { return Business.Common.GetFlatCode(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objResponse;

        }

        public Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsResponse GetDescriptions(Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsRequest objGetDescriptionsRequest)
        {

            Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsResponse objGetDescriptionsResponse;
            try
            {
                objGetDescriptionsResponse = Claro.Web.Logging.ExecuteMethod(objGetDescriptionsRequest.Audit.Session, objGetDescriptionsRequest.Audit.Transaction, () => { return Business.Common.GetDescriptions(objGetDescriptionsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetDescriptionsRequest.Audit.Session, objGetDescriptionsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objGetDescriptionsResponse;

        }

    }
}
