using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Claro.SIACU.Entity.Coliving.Common;
using Claro.SIACU.Entity.Coliving.getSearchCustomer;
using Claro.SIACU.Entity.Coliving.getRetrieveSubscriptions;
using Claro.SIACU.Entity.Coliving.getObtenerDatosClienteColiving;
using Claro.SIACU.Entity.Coliving.getConsultaLineaCuenta;
using Claro.SIACU.Entity.Coliving.getCustomerInfo;
using Claro.SIACU.Entity.Coliving.getListOST;

namespace Claro.SIACU.Web.Service.Coliving
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ColivingService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ColivingService.svc o ColivingService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ColivingService : IColivingService
    {
        public ColivingService()
        {
            log4net.Config.XmlConfigurator.Configure();

        }
        #region SearchCustomer
        /// <summary>
        /// Implementación para la consulta los datos de una linea/cuenta que ha migrado.
        /// </summary>
        /// <param name="objConsultaLineaRequest">oGetCustomerInfoRequest</param>
        /// <returns>objSearchCustomerResponse.</returns>
        public SearchCustomerResponse GetSearchCustomer(Entity.Coliving.getSearchCustomer.SearchCustomerRequest oGetCustomerInfoRequest)
        {
            var objSearchCustomerResponse = new SearchCustomerResponse();
            try
            {
                objSearchCustomerResponse = Claro.Web.Logging.ExecuteMethod<SearchCustomerResponse>(() => { return Business.Coliving.SearchCustomer.GetSearchCustomer(oGetCustomerInfoRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oGetCustomerInfoRequest.Audit.Session, oGetCustomerInfoRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objSearchCustomerResponse;
        }
        /// <summary>
        /// Implementación para devolver las susbcripcion del cliente de una linea/cuenta migrada.
        /// </summary>
        /// <param name="objConsultaLineaRequest">retrieveSubscriptionRequest</param>
        /// <returns>objSearchCustomerResponse</returns>
        public RetrieveSubscriptionResponse GetRetrieveSubscriptions(RetrieveSubscriptionRequest retrieveSubscriptionRequest)
        {
            var objSearchCustomerResponse = new RetrieveSubscriptionResponse();
            try
            {
                objSearchCustomerResponse = Claro.Web.Logging.ExecuteMethod<RetrieveSubscriptionResponse>(() => { return Business.Coliving.SearchCustomer.GetRetrieveSubscriptions(retrieveSubscriptionRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(retrieveSubscriptionRequest.Audit.Session, retrieveSubscriptionRequest.Audit.Transaction, ex.Message);
            }
            return objSearchCustomerResponse;
        }
        /// <summary>
        /// Contrato que consulta los datos una linea/cuenta no migrada.
        /// </summary>
        /// <param name="objConsultaLineaRequest">ObtenerDatosRequests</param>
        /// <returns>objObtenerDatosResponse</returns>
        public ObtenerDatosClienteResponse GetObtenerDatosCliente(ObtenerDatosClienteRequest ObtenerDatosRequest)
        {
            var objObtenerDatosResponse = new ObtenerDatosClienteResponse();
            try
            {
                objObtenerDatosResponse = Claro.Web.Logging.ExecuteMethod<ObtenerDatosClienteResponse>(() =>
                {
                    return Business.Coliving.SearchCustomer.GetObtenerDatosCliente(ObtenerDatosRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(ObtenerDatosRequest.Audit.Session, ObtenerDatosRequest.Audit.Transaction, ex.Message);

            }
            return objObtenerDatosResponse;
        }
        /// <summary>
        /// Implementacion  para obtener el estado de la linea/cuenta si ha migrado o no.
        /// </summary>
        /// <param name="objConsultaLineaRequest">ConsultaLineaRequest</param>
        /// <returns>objSearchCustomerResponse</returns>
        public ConsultaLineaResponse ConsultarLineaCuenta(ConsultaLineaRequest ConsultaLineaRequest)
        {
            ConsultaLineaResponse objSearchCustomerResponse = null;
            try
            {
                objSearchCustomerResponse = Claro.Web.Logging.ExecuteMethod<ConsultaLineaResponse>(() => { return Business.Coliving.SearchCustomer.ConsultarLineaCuenta(ConsultaLineaRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(ConsultaLineaRequest.Audit.Session, ConsultaLineaRequest.Audit.Transaction, ex.Message);

            }
            return objSearchCustomerResponse;
        }
        /// <summary>
        /// Implementación que se requiere para obtener el tipo de la cuenta-tipo de cliente(Masivo-Corporativo).
        /// </summary>
        /// <param name="objConsultaLineaRequest">CustomerInfoRequest</param>
        /// <returns>objCustomerInfoResponse</returns>
        public CustomerInfoResponse GetCustomerInfo(CustomerInfoRequest CustomerInfoRequest)
        {
            CustomerInfoResponse objCustomerInfoResponse = null;
            try
            {
                objCustomerInfoResponse = Claro.Web.Logging.ExecuteMethod<CustomerInfoResponse>(() =>
                {
                    return Business.Coliving.SearchCustomer.GetCustomerInfo(CustomerInfoRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(CustomerInfoRequest.Audit.Session, CustomerInfoRequest.Audit.Transaction, ex.Message);
            }
            return objCustomerInfoResponse;
        }

        public string GetAccountTelephoneCustomer(string Session, string Transaction, string strValue, string strTypeSearch)
        {
            string strNumeroCuenta = string.Empty;
            string strOutTelefono = string.Empty;
            string strplataforma = string.Empty;
            try
            {
                strOutTelefono = Claro.Web.Logging.ExecuteMethod<string>(Session, Transaction, () =>
                {

                    return Business.Coliving.SearchCustomer.GetAccountTelephoneCustomer(
                     Session,
                     Transaction,
                     strValue, strTypeSearch);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(Session,Transaction, ex.Message);
                throw;
            }
            return strOutTelefono;
        }

        #endregion

        #region Common
        public List<ColivingParameters> GetColivingParameters(string strIdSession, string strTransaction)
        {
            List<ColivingParameters> list = null;
            try
            {
                list = Claro.Web.Logging.ExecuteMethod<List<ColivingParameters>>(() =>
                {
                    return Business.Coliving.Common.GetColivingParameters(strIdSession, strTransaction);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return list;
        }
        #endregion


        #region OST - Listar OST

        public ListOSTResponse GetListOST_Legacy(ListOSTRequest objListOSTRequest)
        {
            ListOSTResponse objListOSTResponse = null;
            try
            {
                objListOSTResponse = Claro.Web.Logging.ExecuteMethod<ListOSTResponse>(() =>
                {
                    return Business.Coliving.SearchCustomer.getListOST_Legacy(objListOSTRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objListOSTResponse;
        }

        public ListOSTResponse GetListOST_One(ListOSTRequest objListOSTRequest)
        {
            ListOSTResponse objListOSTResponse = null;
            try
            {
                objListOSTResponse = Claro.Web.Logging.ExecuteMethod<ListOSTResponse>(() =>
                {
                    return Business.Coliving.SearchCustomer.getListOST_One(objListOSTRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objListOSTResponse;
        }


        public ListOSTResponse GetListOSTDetails_One(ListOSTRequest objListOSTRequest)
        {
            ListOSTResponse objListOSTResponse = null;
            try
            {
                objListOSTResponse = Claro.Web.Logging.ExecuteMethod<ListOSTResponse>(() =>
                {
                    return Business.Coliving.SearchCustomer.getListOSTDetails_One(objListOSTRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objListOSTResponse;
        }

        #endregion

    }
}      