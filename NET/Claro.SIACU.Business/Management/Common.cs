using Claro.SIACU.Entity.Management;
using Claro.SIACU.Entity.Management.GetInstant;
using Claro.SIACU.Entity.Management.GetListInstant;
using System;
using System.Collections.Generic;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Business.Management
{
    public static class Common
    {
        /// <summary>
        /// Método para insertar las instantáneas.
        /// </summary>
        /// <param name="objInsertInstantRequest">Contiene datos de la instantánea.</param>
        /// <returns>Devuelve objeto InstantResponse con el resultado de la transacción.</returns>
        public static Entity.Management.GetInstant.InstantResponse InsertInstant(InstantRequest objInsertInstantRequest)
        {
            string message = "";
            Entity.Management.GetInstant.InstantResponse objInsertInstantResponse = new Entity.Management.GetInstant.InstantResponse()
            {
                result = Claro.Web.Logging.ExecuteMethod<bool>(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, () => { return Data.Management.Common.InsertInstant(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, objInsertInstantRequest.instant, out message); }),
                message = message
            };

            return objInsertInstantResponse;
        }


        /// <summary>
        /// Método para insertar las instantáneas.
        /// </summary>
        /// <param name="objInsertInstantRequest">Contiene datos de la instantánea.</param>
        /// <returns>Devuelve objeto InstantResponse con el resultado de la transacción.</returns>
        public static Entity.Management.GetInstant.InstantResponse InsertInstants(InstantRequest objInsertInstantRequest)
        {
            string message = "";
            Entity.Management.GetInstant.InstantResponse objInsertInstantResponse = null;
            foreach (var item in objInsertInstantRequest.lstInstant)
            {
                try
                {
                    objInsertInstantResponse = new Entity.Management.GetInstant.InstantResponse()
                    {
                        result = Claro.Web.Logging.ExecuteMethod<bool>(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, () => { return Data.Management.Common.InsertInstant(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, item, out message); }),
                        message = message
                    };
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, ex.Message);
                    throw;
                }
            }

            return objInsertInstantResponse;
        }

        /// <summary>
        /// Método para actualizar datos de la instantánea.
        /// </summary>
        /// <param name="objUpdateInstantRequest">Contiene datos de la instantánea.</param>
        /// <returns>Devuelve objeto InstantResponse con el resultado de la transacción.</returns>
        public static Entity.Management.GetInstant.InstantResponse UpdateInstant(InstantRequest objUpdateInstantRequest)
        {
            string message = "";
            Entity.Management.GetInstant.InstantResponse objUpdateInstantResponse = new Entity.Management.GetInstant.InstantResponse()
            {
                result = Claro.Web.Logging.ExecuteMethod<bool>(objUpdateInstantRequest.Audit.Session, objUpdateInstantRequest.Audit.Transaction, () => { return Data.Management.Common.UpdateInstant(objUpdateInstantRequest.Audit.Session, objUpdateInstantRequest.Audit.Transaction, objUpdateInstantRequest.instant, out message); }),
                message = message
            };
            return objUpdateInstantResponse;
        }

        /// <summary>
        /// Método para desactivar la instantánea.
        /// </summary>
        /// <param name="objDeactivateInstantRequest">Contiene datos de la instantánea.</param>
        /// <returns>Devuelve objeto InstantResponse con el resultado de la transacción.</returns>
        public static Entity.Management.GetInstant.InstantResponse DeactivateInstant(InstantRequest objDeactivateInstantRequest)
        {
            string message = string.Empty;
            Entity.Management.GetInstant.InstantResponse objDeactivateInstantResponse = new Entity.Management.GetInstant.InstantResponse()
            {
                result = Claro.Web.Logging.ExecuteMethod<bool>(objDeactivateInstantRequest.Audit.Session, objDeactivateInstantRequest.Audit.Transaction, () => { return Data.Management.Common.DeactivateInstant(objDeactivateInstantRequest.Audit.Session, objDeactivateInstantRequest.Audit.Transaction, objDeactivateInstantRequest.instant, out message); }),
                message = message
            };
            return objDeactivateInstantResponse;
        }

        /// <summary>
        /// Método para obtener listado de instantáneas.
        /// </summary>
        /// <param name="objListInstantRequestRequest">Contiene tipo de teléfono y fecha.</param>
        /// <returns>Devuelve listado de objetos InstantResponse con el resultado de la transacción.</returns>
        public static Entity.Management.GetListInstant.ListInstantResponse ListInstant(ListInstantRequest objListInstantRequestRequest)
        {
            Entity.Management.GetListInstant.ListInstantResponse objListInstantResponse = new Entity.Management.GetListInstant.ListInstantResponse()
            {
                listInstant = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Instant>>(objListInstantRequestRequest.Audit.Session, objListInstantRequestRequest.Audit.Transaction, () =>
                {
                    return Data.Management.Common.ListInstant(
                    objListInstantRequestRequest.Audit.Session, objListInstantRequestRequest.Audit.Transaction,
                    objListInstantRequestRequest.vDato, objListInstantRequestRequest.vTipoTelefono, objListInstantRequestRequest.fecha);
                }),
            };
            return objListInstantResponse;
        }

        /// <summary>
        /// Método para obtener datos de la instantánea por id.
        /// </summary>
        /// <param name="objInstantByIdRequest">Contiene id de instanánea.</param>
        /// <returns>Devuelve objeto InstantByIdResponse con la información de la isntantánea.</returns>
        public static Entity.Management.GetInstantById.InstantByIdResponse GetinstantById(Entity.Management.GetInstantById.InstantByIdRequest objInstantByIdRequest)
        {
            Entity.Management.GetInstantById.InstantByIdResponse objInstantByIdResponse = new Entity.Management.GetInstantById.InstantByIdResponse
            {
                instant = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Instant>(objInstantByIdRequest.Audit.Session, objInstantByIdRequest.Audit.Transaction, () =>
                {
                    return Data.Management.Common.GetinstantById(
                    objInstantByIdRequest.Audit.Session, objInstantByIdRequest.Audit.Transaction,
                    objInstantByIdRequest.IdInstant);
                }),

            };
            return objInstantByIdResponse;
        }

        /// <summary>
        /// Método para obtener datos del banner
        /// </summary>
        /// <param name="objGetBannerRequest">Contiene fecha, tipo de teléfono y estado.</param>
        /// <returns>Devuelve objeto BannerResponse con la información del banner.</returns>
        public static Entity.Management.GetBanner.BannerResponse GetBanner(Entity.Management.GetBanner.BannerRequest objGetBannerRequest)
        {
            Entity.Management.GetBanner.BannerResponse objGetBannerResponse = new Entity.Management.GetBanner.BannerResponse()
            {
                ListBanner = Claro.Web.Logging.ExecuteMethod<List<Banner>>(objGetBannerRequest.Audit.Session, objGetBannerRequest.Audit.Transaction, () => { return Data.Management.Common.GetBanner(objGetBannerRequest.Audit.Session, objGetBannerRequest.Audit.Transaction, objGetBannerRequest.Date, objGetBannerRequest.Status, KEY.AppSettings("strTipoTelefonoPostPago")); }),
            };

            return objGetBannerResponse;
        }

        public static Entity.Management.GetCreate.CreateResponse GetCreate(Entity.Management.GetCreate.CreateRequest objGetCreateRequest)
        {
            Entity.Management.GetCreate.CreateResponse objGetCreateResponse = null;

            if (Data.Management.Common.GetCreate(objGetCreateRequest.Audit.Session, objGetCreateRequest.Audit.Transaction, objGetCreateRequest.ID_BANNER, objGetCreateRequest.MENSAJE, objGetCreateRequest.FECHA_VIGENCIA_INICIO, objGetCreateRequest.FECHA_VIGENCIA_FIN, objGetCreateRequest.LOGIN, objGetCreateRequest.ORDEN_PRIORIDAD, KEY.AppSettings("strTipoTelefonoPostPago")))
            {
                objGetCreateResponse = new Entity.Management.GetCreate.CreateResponse()
                {
                    ListBanner = Claro.Web.Logging.ExecuteMethod<List<Banner>>(objGetCreateRequest.Audit.Session, objGetCreateRequest.Audit.Transaction, () => { return Data.Management.Common.GetBanner(objGetCreateRequest.Audit.Session, objGetCreateRequest.Audit.Transaction, objGetCreateRequest.DATE, objGetCreateRequest.STATUS, KEY.AppSettings("strTipoTelefonoPostPago")); }),
                };
            }

            return objGetCreateResponse;
        }

        public static Entity.Management.GetModify.ModifyResponse GetModify(Entity.Management.GetModify.ModifyRequest objGetModifyRequest)
        {
            Entity.Management.GetModify.ModifyResponse objGetModifyResponse = null;

            if (Data.Management.Common.GetModify(objGetModifyRequest.Audit.Session, objGetModifyRequest.Audit.Transaction, objGetModifyRequest.ID_BANNER, objGetModifyRequest.MENSAJE, objGetModifyRequest.FECHA_VIGENCIA_INICIO, objGetModifyRequest.FECHA_VIGENCIA_FIN, objGetModifyRequest.LOGIN, objGetModifyRequest.ORDEN_PRIORIDAD, KEY.AppSettings("strTipoTelefonoPostPago")))
            {
                objGetModifyResponse = new Entity.Management.GetModify.ModifyResponse()
                {
                    ListBanner = Claro.Web.Logging.ExecuteMethod<List<Banner>>(objGetModifyRequest.Audit.Session, objGetModifyRequest.Audit.Transaction, () => { return Data.Management.Common.GetBanner(objGetModifyRequest.Audit.Session, objGetModifyRequest.Audit.Transaction, objGetModifyRequest.DATE, objGetModifyRequest.STATUS, KEY.AppSettings("strTipoTelefonoPostPago")); }),
                };
            }

            return objGetModifyResponse;
        }

        public static Entity.Management.GetBannerId.BannerIdResponse GetBannerId(Entity.Management.GetBannerId.BannerIdRequest objGetBannerIdRequest)
        {
            Entity.Management.GetBannerId.BannerIdResponse objGetBannerIdResponse = new Entity.Management.GetBannerId.BannerIdResponse
            {
                Banner = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Management.Banner>(objGetBannerIdRequest.Audit.Session, objGetBannerIdRequest.Audit.Transaction, () =>
                {
                    return Data.Management.Common.GetBannerId(
                    objGetBannerIdRequest.Audit.Session, objGetBannerIdRequest.Audit.Transaction,
                    objGetBannerIdRequest.ID_BANNER);
                }),

            };
            return objGetBannerIdResponse;
        }

        public static Entity.Management.GetDeactivate.DeactivateResponse GetDeactivate(Entity.Management.GetDeactivate.DeactivateRequest objGetDeactivateRequest)
        {
            Entity.Management.GetDeactivate.DeactivateResponse objGetDeactivateResponse = null;

            if (Data.Management.Common.GetDeactivate(objGetDeactivateRequest.Audit.Session, objGetDeactivateRequest.Audit.Transaction, objGetDeactivateRequest.ID_BANNER, objGetDeactivateRequest.LOGIN))
            {
                objGetDeactivateResponse = new Entity.Management.GetDeactivate.DeactivateResponse()
                {
                    ListBanner = Claro.Web.Logging.ExecuteMethod<List<Banner>>(objGetDeactivateRequest.Audit.Session, objGetDeactivateRequest.Audit.Transaction, () => { return Data.Management.Common.GetBanner(objGetDeactivateRequest.Audit.Session, objGetDeactivateRequest.Audit.Transaction, objGetDeactivateRequest.DATE, objGetDeactivateRequest.STATUS, KEY.AppSettings("strTipoTelefonoPostPago")); }),
                };
            }

            return objGetDeactivateResponse;
        }

        public static Entity.Management.GetInstant.InstantResponse GetInstantCorrelative(InstantRequest objInsertInstantRequest, string strLogin)
        {
            string rMsgCorrelativeText = "";
            Entity.Management.GetInstant.InstantResponse objInsertInstantResponse = new Entity.Management.GetInstant.InstantResponse()
            {
                result = Claro.Web.Logging.ExecuteMethod<bool>(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, () => { return Data.Management.Common.GetInstantCorrelative(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, strLogin, out rMsgCorrelativeText); }),
                message = rMsgCorrelativeText
            };
            return objInsertInstantResponse;
        }

        public static Entity.Management.GetInstant.InstantResponse SaveIntantsMasive(InstantRequest objInsertInstantRequest)
        {
            decimal rIdGenerado = 0;
            string rHeader = "";
            Entity.Management.GetInstant.InstantResponse objInsertInstanHeadertResponse = new Entity.Management.GetInstant.InstantResponse()
            {
                result = Claro.Web.Logging.ExecuteMethod<bool>(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, () => { return Data.Management.Common.SaveInstantMasiveHeader(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, objInsertInstantRequest.instant, out rIdGenerado, out rHeader); }),
            };

            if (rIdGenerado != 0 || rHeader != "")
            {
                Entity.Management.GetInstant.InstantResponse objInstantDetailResponse;
                try
                {
                    foreach (var item in objInsertInstantRequest.lstInstant)
                    {
                        objInstantDetailResponse = new Entity.Management.GetInstant.InstantResponse()
                        {
                            result = Claro.Web.Logging.ExecuteMethod<bool>(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, () => { return Data.Management.Common.SaveIntantMasiveDetail(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, rIdGenerado, objInsertInstantRequest.Audit.UserName, item); }),
                        };

                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objInsertInstantRequest.Audit.Session, objInsertInstantRequest.Audit.Transaction, ex.Message);
                    throw;
                }
            }//aca 

            return objInsertInstanHeadertResponse;
        }
    }
}
