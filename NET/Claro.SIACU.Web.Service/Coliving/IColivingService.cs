using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
namespace Claro.SIACU.Web.Service.Coliving
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IColivingService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IColivingService
    {
        #region SearchCustomer
        /// <summary>
        /// Contrato para la consulta los datos de una linea/cuenta que ha migrado.
        /// </summary>
        /// <param name="objConsultaLineaRequest">oGetCustomerInfoRequest</param>
        /// <returns></returns>
        [OperationContract]
        Claro.SIACU.Entity.Coliving.getSearchCustomer.SearchCustomerResponse GetSearchCustomer(Claro.SIACU.Entity.Coliving.getSearchCustomer.SearchCustomerRequest oGetCustomerInfoRequest);
        /// <summary>
        /// Contrato para devolver las susbcripcion del cliente de una linea/cuenta migrada.
        /// </summary>
        /// <param name="objConsultaLineaRequest">retrieveSubscriptionRequest</param>
        /// <returns></returns>
        [OperationContract]
        Claro.SIACU.Entity.Coliving.getRetrieveSubscriptions.RetrieveSubscriptionResponse GetRetrieveSubscriptions(Claro.SIACU.Entity.Coliving.getRetrieveSubscriptions.RetrieveSubscriptionRequest retrieveSubscriptionRequest);
        /// <summary>
        /// Contrato  para obtener el estado de la linea/cuenta si ha migrado o no.
        /// </summary>
        /// <param name="objConsultaLineaRequest">ConsultaLineaRequest</param>
        /// <returns></returns>
        [OperationContract]
        Claro.SIACU.Entity.Coliving.getConsultaLineaCuenta.ConsultaLineaResponse ConsultarLineaCuenta(Claro.SIACU.Entity.Coliving.getConsultaLineaCuenta.ConsultaLineaRequest ConsultaLineaRequest);
        /// <summary>
        /// Contrato que consulta los datos una linea/cuenta no migrada.
        /// </summary>
        /// <param name="objConsultaLineaRequest">ObtenerDatosRequest</param>
        /// <returns></returns>
        [OperationContract]
        Claro.SIACU.Entity.Coliving.getObtenerDatosClienteColiving.ObtenerDatosClienteResponse GetObtenerDatosCliente(Claro.SIACU.Entity.Coliving.getObtenerDatosClienteColiving.ObtenerDatosClienteRequest ObtenerDatosRequest);
        /// <summary>
        /// Contrato que se requiere para obtener el tipo de la cuenta-tipo de cliente(Masivo-Corporativo).
        /// </summary>
        /// <param name="objConsultaLineaRequest">CustomerInfoRequest</param>
        /// <returns></returns>
        [OperationContract]
        Claro.SIACU.Entity.Coliving.getCustomerInfo.CustomerInfoResponse GetCustomerInfo(Claro.SIACU.Entity.Coliving.getCustomerInfo.CustomerInfoRequest CustomerInfoRequest);

        [OperationContract]
        string GetAccountTelephoneCustomer(string Session, string Transaction, string strValue, string strTypeSearch);


        #endregion

        #region Common
        [OperationContract]
        List<Claro.SIACU.Entity.Coliving.Common.ColivingParameters> GetColivingParameters(string strIdSession, string strTransaction);
        #endregion


        #region OST - Listar OST

        [OperationContract]
        Claro.SIACU.Entity.Coliving.getListOST.ListOSTResponse GetListOST_Legacy(Claro.SIACU.Entity.Coliving.getListOST.ListOSTRequest objListOSTRequest);

        [OperationContract]
        Claro.SIACU.Entity.Coliving.getListOST.ListOSTResponse GetListOST_One(Claro.SIACU.Entity.Coliving.getListOST.ListOSTRequest objListOSTRequest);

        [OperationContract]
        Claro.SIACU.Entity.Coliving.getListOST.ListOSTResponse GetListOSTDetails_One(Claro.SIACU.Entity.Coliving.getListOST.ListOSTRequest objListOSTRequest);

        #endregion

    }
}
