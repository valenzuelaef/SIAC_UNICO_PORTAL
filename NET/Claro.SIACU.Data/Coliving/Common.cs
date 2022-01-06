using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.Data;
using System.Data;
using Claro.SIACU.Entity.Coliving.Common;
using Claro.SIACU.Data.Configuration;
namespace Claro.SIACU.Data.Coliving
{
    public static class Common
    {

        #region Coliving_Parameters

        /// <summary>
        /// Método que obtiene una lista de valores de una tabla de configuración
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <returns>Devuelve listado de objetos ColivingParameters.</returns>

        public static List<ColivingParameters> GetColivingParameters(string strIdSession, string strTransaction)
        {
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Begin a GetColivingParameters Capa Data");
            List<ColivingParameters> lstColivingParameters = new List<ColivingParameters>();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("C_SIACALL", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_CODIGO_SALIDA",DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_MENSAJE_SALIDA", DbType.String, 10, ParameterDirection.Output)
            };
            try
            {
                lstColivingParameters = DbFactory.ExecuteReader<List<ColivingParameters>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_SIACSS_PARAMETROS, parameters);

                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, String.Format("End a GetColivingParameters   Capa Data -> Parametro de salida: lstColivingParameters : {0}", (lstColivingParameters.Count == 0) ? "0" : lstColivingParameters.Count.ToString()).ToString());
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, "(Exception) Error :" + ex.Message);
            }
            return lstColivingParameters;
        }
        #endregion
    }
}