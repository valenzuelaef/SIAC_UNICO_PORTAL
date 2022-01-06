using Claro.SIACU.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Claro.SIACU.Business
{
    public static class Helper
    {

        /// <summary>
        /// Método para obtener el número de recibo.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <param name="strFechaEmision">Fecha de emisión</param>
        /// <returns>Devuelve cadena con número de recibo.</returns>
        public static string GetNumberReceipt(string strIdSession, string strTransactionID, string strInvoiceNumber, string strFechaEmision)
        {
            string strNroReciboTemp = "";
            int intLongitud = Claro.Constants.NumberZero;
            string strParteNro = "";
            string sFechaEmisionCorta = "";
            string sFechaEmision = strFechaEmision.Trim();
            string strFechaCorte = Claro.SIACU.Constants.Receipt8;
            string strFechaCorte1 = Claro.SIACU.Constants.Receipt9;
            string lSalida = "";
            string lSalida7 = "";
            string lSalida8 = "";
            string lSalida9 = "";
            string SB01_ = "SB01-";
            string strFechaVigenciaSB01 = Claro.ConfigurationManager.AppSettings("strFechaVigenciaSB01");
            DateTime FechaSB01, FechaEmision;
            try
            {
                Claro.Web.Logging.Info(strIdSession, strTransactionID, "strFechaVigenciaSB01 : " + strFechaVigenciaSB01.ToString());
                Claro.Web.Logging.Info(strIdSession, strTransactionID, "strFechaEmision : " + strFechaEmision.ToString());
                if (DateTime.TryParse(strFechaVigenciaSB01, out FechaSB01) && DateTime.TryParse(strFechaEmision, out FechaEmision) && FechaEmision >= FechaSB01)
                {
                    sFechaEmisionCorta = (sFechaEmision.Length > 8 ? sFechaEmision.Substring(6, 4) + sFechaEmision.Substring(3, 2) + sFechaEmision.Substring(0, 2) : sFechaEmision);
                    strNroReciboTemp = strInvoiceNumber.Substring(0, strInvoiceNumber.Length - 6);
                    intLongitud = strNroReciboTemp.Length;

                    if (intLongitud >= 7)
                    {
                        lSalida7 = SB01_ + strNroReciboTemp.Substring(intLongitud - 7);
                        lSalida8 = SB01_ + strNroReciboTemp.Substring(intLongitud - 8);
                        lSalida9 = SB01_ + strNroReciboTemp.Substring(0, 10);
                    }
                    else
                    {
                        for (int i = 1; i <= 7 - intLongitud; i++) strParteNro = Claro.Constants.NumberZeroString + strParteNro;
                        lSalida7 = SB01_ + strParteNro + strNroReciboTemp;
                        for (int i = 1; i <= 8 - intLongitud; i++) strParteNro = Claro.Constants.NumberZeroString + strParteNro;
                        lSalida8 = SB01_ + strParteNro + strNroReciboTemp;
                    }

                    var comparacionFechas = string.Compare(sFechaEmisionCorta, strFechaCorte, StringComparison.InvariantCulture);

                    if (comparacionFechas < 0)
                    {
                        lSalida = lSalida7;
                    }
                    else
                    {
                        comparacionFechas = string.Compare(sFechaEmisionCorta, strFechaCorte1, StringComparison.InvariantCulture);
                        lSalida = (comparacionFechas < 0 ? lSalida8 : lSalida9);
                    }
                   
                }
                else
                {
                sFechaEmisionCorta = (sFechaEmision.Length > 8 ? sFechaEmision.Substring(6, 4) + sFechaEmision.Substring(3, 2) + sFechaEmision.Substring(0, 2) : sFechaEmision);
                strNroReciboTemp = strInvoiceNumber.Substring(0, strInvoiceNumber.Length - 6);
                intLongitud = strNroReciboTemp.Length;

                if (intLongitud >= 7)
                {
                    lSalida7 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(intLongitud - 7);
                    lSalida8 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(intLongitud - 8);
                    lSalida9 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(0, 10);
                }
                else
                {
                    for (int i = 1; i <= 7 - intLongitud; i++) strParteNro = Claro.Constants.NumberZeroString + strParteNro;
                    lSalida7 = Claro.SIACU.Constants.T001_ + strParteNro + strNroReciboTemp;
                    for (int i = 1; i <= 8 - intLongitud; i++) strParteNro = Claro.Constants.NumberZeroString + strParteNro;
                    lSalida8 = Claro.SIACU.Constants.T001_ + strParteNro + strNroReciboTemp;
                }

                var comparacionFechas = string.Compare(sFechaEmisionCorta, strFechaCorte, StringComparison.InvariantCulture);

                if (comparacionFechas < 0)
                {
                    lSalida = lSalida7;
                }
                else
                {
                    comparacionFechas = string.Compare(sFechaEmisionCorta, strFechaCorte1, StringComparison.InvariantCulture);
                    lSalida = (comparacionFechas < 0 ? lSalida8 : lSalida9);
                }
            }
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }
            Claro.Web.Logging.Info(strIdSession, strTransactionID, "lSalida : " + lSalida.ToString());
            return lSalida;
        }

        /// <summary>
        /// Método para obtener número TFI.
        /// </summary>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <returns>Devuelve cadena con número TFI.</returns>
        public static string ReturnTFI(string strTelephone)
        {
            string strValueSearch = "";

            if (strTelephone.Length != 9) strValueSearch = strTelephone.PadLeft(11, char.Parse(Claro.Constants.NumberZeroString));
            else strValueSearch = strTelephone;
            return strValueSearch.Trim();
        }

        /// <summary>
        /// Método para obtener una lista de un archivo Xml.
        /// </summary>
        /// <param name="strNameFunction">Nombre de función</param>
        /// <returns>Devuelve lista de objetos con información del archivo xml.</returns>
        private static List<ListItem> GetListXML(string strNameFunction)
        {
            List<ListItem> listGenericItem = new List<ListItem>();

            string file = "Data.xml";
            string strApplicationPath;
            strApplicationPath = new Claro.Utils().GetApplicationPath();

            string fullPath = strApplicationPath + file;

            XmlDocument documento = new XmlDocument();
            documento.Load(fullPath);
            XmlNodeList nodos = documento.SelectNodes("descendant::" + strNameFunction + "/item");

            ListItem oGenericItem;
            foreach (XmlNode nodo in nodos)
            {
                oGenericItem = new ListItem();
                oGenericItem.Code = nodo["codigo"].InnerText;
                oGenericItem.Description = nodo["descripcion"].InnerText;
                listGenericItem.Add(oGenericItem);
            }

            return listGenericItem;
        }

        /// <summary>
        /// Método para obtener un valor de un archivo Xml.
        /// </summary>
        /// <param name="strValue">valor</param>
        /// <param name="strNameFunction">Nombre de la función</param>
        /// <returns>Devuelve cadena con valor de un archivo xml.</returns>
        public static string GetValueXML(string strValue, string strNameFunction)
        {
            string response = string.Empty;

            List<ListItem> list = GetListXML(strNameFunction);

            var listResult = from m in list
                             where m.Code.Equals(strValue)
                             select m;

            foreach (ListItem p in listResult)
            {
                response = p.Description.ToString();
                break;
            }

            return response;
        }


    }
}